using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PersonApi.Entities;
using PersonApi.Entities.Exceptions;
using PersonApi.Models;
using PersonApi.Models.Dto;
using PersonApi.Models.Exceptions;
using PersonApi.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PersonApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IConfiguration _config;
        private readonly ITwilioService _twilioService;


        public AccountService(IPersonRepository personRepository, IConfiguration config, ITwilioService twilioService)
        {
            _personRepository = personRepository;
            _config = config;
            _twilioService = twilioService;
        }

        public async Task<AuthToken> LogInAsync(LoginDto loginDto)
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

            var token = GenerateToken(person.Login, person.Id);

            AuthToken authToken = new AuthToken
            {
                Token = token,
                Id = person.Id
            };

            return authToken;
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
                IsPhoneVerified = false
            };

            Random random = new Random();
            int code = random.Next(1000, 10000);

            var sms = new SmsDto
            {
                Message = $"Hello! Your verification code is {code}",
                PhoneNumber = signUpDto.PhoneNumber.Trim()
            };

            await _twilioService.SendSmsAsync(sms);

            await _personRepository.AddPersonAsync(person);

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

           /* var token = GenerateToken(person.Login, person.Id);

            AuthToken authToken = new AuthToken
            {
                Token = token,
                Id = person.Id
            };

            return authToken;*/
        }

        public async Task<AuthToken> VerifyPhoneNumber(CheckSmsCodeDto checkSmsCodeDto)
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

            var token = GenerateToken(person.Login, person.Id);

            AuthToken authToken = new AuthToken
            {
                Token = token,
                Id = person.Id
            };

            return authToken;
        }

        public string GenerateToken(string login, long id)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretData:secretKey"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new("login", login),
                new("id", id.ToString()),
            };

            var expires = DateTime.Now + new TimeSpan(0, 0, 0, int.Parse(_config["JwtExpiresSec"]));

            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: expires);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public async Task<Person> GetPersonByIdAsync(long id)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);

            if(person == null)
            {
                throw new NotFoundException("User doesn't exist");
            }

            return person;
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
        
    }
}
