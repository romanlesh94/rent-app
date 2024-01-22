using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PersonApi.Entities;
using PersonApi.Entities.Exceptions;
using PersonApi.Models;
using PersonApi.Models.Dto;
using PersonApi.Models.Enums;
using PersonApi.Models.Exceptions;
using PersonApi.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PersonApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IConfiguration _config;
        private readonly IRabbitMqProducer _rabbitMqProducer;
        private readonly ITokenService _tokenService;


        public AccountService(IPersonRepository personRepository, IConfiguration config, IRabbitMqProducer rabbitMqProducer,
            ITokenService tokenService)
        {
            _personRepository = personRepository;
            _config = config;
            _rabbitMqProducer = rabbitMqProducer;
            _tokenService = tokenService;
        }

        private const long FileMaxSize = 5242880;

        public async Task<TokensDto> LogInAsync(LoginDto loginDto)
        {
            var person = await _personRepository.GetPersonAsync(loginDto.Login);

            if (person == null)
            {
                throw new NotFoundException("User not found!");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, person.Password))
            {
                throw new InternalException("Wrong password!");
            }

            var token = GenerateToken(person.Login, person.Id, person.Role.ToString());

            var refreshToken = new RefreshToken
            {
                Token = _tokenService.GenerateRefreshToken(),
                Person = person,
                CreatedDtm = DateTime.Now
            };

            if(person.RefreshToken != null)
            {
                await _personRepository.RemoveRefreshTokenAsync(person.RefreshToken.Id);
            }

            await _personRepository.SaveRefreshTokenAsync(refreshToken);

            TokensDto tokens = new TokensDto
            {
                PersonId = person.Id,
                AuthToken = token,
                RefreshToken = refreshToken.Token,
                Role = person.Role
            };

            return tokens;
        }   

        public async Task<long> SignUpAsync(SignUpDto signUpDto)
        {
            var dbPerson = await _personRepository.GetPersonAsync(signUpDto.Login);

            if(dbPerson != null)
            {
                throw new InternalException("This username is already taken!");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(signUpDto.Password.Trim());

            Person person = new Person
            {
                Login = signUpDto.Login.Trim(),
                Password = passwordHash,
                Email = signUpDto.Email.Trim(),
                Country = signUpDto.Country.Trim(),
                PhoneNumber = signUpDto.PhoneNumber.Trim(),
                IsPhoneVerified = false,
                Role = Roles.User,
            };

            var refreshToken = new RefreshToken
            {
                Token = _tokenService.GenerateRefreshToken(),
                Person = person,
                CreatedDtm = DateTime.Now
            };

            Random random = new Random();
            int code = random.Next(1000, 10000);

            var message = new MessageDto
            {
                Code = code.ToString(),
                PhoneNumber = signUpDto.PhoneNumber
            };

            await _rabbitMqProducer.SendSmsMessage(message);

            await _personRepository.AddPersonAsync(person);
            await _personRepository.SaveRefreshTokenAsync(refreshToken);

            var createdPerson = await _personRepository.GetPersonAsync(person.Login);

            var verification = new PhoneVerification
            {
                PersonId = createdPerson.Id,
                Code = code.ToString(),
                CreatedDate = DateTime.UtcNow,
                IsVerified = false, 
            };

            await _personRepository.AddPhoneVerificationAsync(verification);

            return createdPerson.Id;
        }

        public async Task<TokensDto> VerifyPhoneNumber(CheckSmsCodeDto checkSmsCodeDto)
        {
            var person = await _personRepository.GetPersonByIdAsync(checkSmsCodeDto.PersonId);

            if (person == null) { throw new NotFoundException("This user doesn't exist!"); };

            var verification = await _personRepository.GetPhoneVerification(checkSmsCodeDto.PersonId);

            if (verification.Code != checkSmsCodeDto.Code)
            {
                throw new InternalException("The code you have sent is incorrect!");
            }

            verification.IsVerified = true;
            person.IsPhoneVerified = true;

            await _personRepository.UpdatePhoneVerification(verification);
            await _personRepository.UpdatePersonAsync(person);

            var token = GenerateToken(person.Login, person.Id, person.Role.ToString());

            TokensDto tokens = new TokensDto
            {
                PersonId = person.Id,
                AuthToken = token,
                RefreshToken = person.RefreshToken.Token,
                Role = person.Role
            };

            return tokens;
        }

        public async Task AddPersonImageAsync(long personId, IFormFile file)
        {
            var person = await _personRepository.GetPersonByIdAsync(personId);

            if (person == null)
            {
                throw new NotFoundException("Person is not found!");
            }

            if (file.Length > FileMaxSize)
            {
                throw new InternalException("File size is more than 5mb");
            }

            if (file.Length == 0)
            {
                throw new InternalException("File length is 0");
            }

            var storageClient = await StorageClient.CreateAsync(GoogleCredential.FromFile(_config.GetValue<string>("GoogleCredentialFile")));
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string objectName = $"images/{fileName}";

            using (var imageMemoryStream = new MemoryStream())
            {
                await file.CopyToAsync(imageMemoryStream);
                imageMemoryStream.Position = 0;
                await storageClient.UploadObjectAsync(_config["GoogleCloudStorageBucket"], objectName, null, imageMemoryStream);
            }

            var storageObject = await storageClient.GetObjectAsync(_config["GoogleCloudStorageBucket"], objectName);

            var image = new PersonImage
            {
                Person = person,
                ImageUrl = storageObject.MediaLink
            };

            await _personRepository.AddPersonImageAsync(image);
        }

        public string GenerateToken(string login, long id, string role)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretData:secretKey"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new("login", login),
                new("id", id.ToString()),
                new("scope", role)
            };

            var expires = DateTime.Now + new TimeSpan(0, 0, 0, int.Parse(_config["JwtExpiresSec"]));

            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: expires);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public async Task<GetPersonDto> GetPersonByIdAsync(long id)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);

            if (person == null)
            {
                throw new NotFoundException("User doesn't exist");
            }

            var image = await _personRepository.GetPersonImageAsync(id);

            GetPersonDto getPerson = new GetPersonDto
            {
                Id = person.Id,
                Country = person.Country,
                Email = person.Email,
                IsPhoneVerified = person.IsPhoneVerified,
                Login = person.Login,
                Password = person.Password,
                PhoneNumber = person.PhoneNumber,
                ImageUrl = image != null ? image.ImageUrl : null,
            };

            return getPerson;
        }

        public async Task UpdatePersonAsync(UpdatePersonDto person)
        {
            var existingPerson = await _personRepository.GetPersonByIdAsync(person.Id);

            if(existingPerson == null)
            {
                throw new NotFoundException("User doesn't exist");
            }

            existingPerson.Login = person.Login;
            existingPerson.Email = person.Email;
            existingPerson.PhoneNumber = person.PhoneNumber;
            existingPerson.Country = person.Country;


            await _personRepository.UpdatePersonAsync(existingPerson);
        }

        public async Task<TokensDto> RefreshTokenVerificationAsync(string refreshToken)
        {
            var person = await _personRepository.GetPersonByRefreshTokenAsync(refreshToken);

            if(person == null) { throw new InternalException("Invalid refresh token"); }

            var refreshTokenExpiresDays = int.Parse(_config["RefreshTokenExpiresDays"]);

            if (DateTime.Now - person.RefreshToken.CreatedDtm > new TimeSpan(refreshTokenExpiresDays, 0, 0, 0))
            {
                await _personRepository.RemoveRefreshTokenAsync(person.RefreshToken.Id);

                throw new InternalException("Invalid refresh token.");
            }

            var newRefreshToken = new RefreshToken
            {
                Token = _tokenService.GenerateRefreshToken(),
                CreatedDtm = DateTime.Now,
                Person = person
            };

            await _personRepository.SaveRefreshTokenAsync(newRefreshToken);
            var newAuthToken = GenerateToken(person.Login, person.Id, person.Role.ToString());

            return new TokensDto
            {
                AuthToken = newAuthToken,
                RefreshToken = newRefreshToken.Token,
                PersonId = person.Id,
                Role = person.Role
            };
        }

        public async Task SendRoleChangeRequest(long personId)
        {
            var existingRequest = await _personRepository.GetRoleChangeRequestAsync(personId);

            if (existingRequest != null) { throw new InternalException("Your request has already been sent!"); }

            var newRequest = new RoleChangeRequest
            {
                PersonId = personId,
                CreatedDate = DateTime.Now,
                IsApproved = false
            };

            await _personRepository.AddRoleChangeRequestAsync(newRequest);
        }

        public async Task<List<RoleChangeRequest>> GetAllPendingRequests()
        {
            return await _personRepository.GetAllPendingRequestsAsync();
        }

        public async Task ApproveRoleChangeRequest(long personId)
        {
            var request = await _personRepository.GetRoleChangeRequestAsync(personId);

            if (request == null) { throw new NotFoundException("The request doesn't exist!"); }

            var person = await _personRepository.GetPersonByIdAsync(personId);

            if (person == null) { throw new NotFoundException("The request doesn't exist!"); }

            request.IsApproved = true;
            await _personRepository.UpdateRoleChangeRequestAsync(request);

            person.Role = Roles.HouseOwner;
            await _personRepository.UpdatePersonAsync(person);
        }
    }
}
