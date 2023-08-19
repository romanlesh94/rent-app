using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations;
using rent.Entities;
using rent.Entities.Dto;
using rent.Entities.Exceptions;
using rent.Entities.Settings;
using rent.Models;
using rent.Models.Dto;
using rent.Models.Exceptions;
using rent.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace rent.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPersonRepository _personRepository;
        private readonly TokenParameters _tokenParameters;
        private readonly IConfiguration _config;


        public AccountService(IPersonRepository personRepository, IOptions<TokenParameters> tokenParameters, IConfiguration config)
        {
            _personRepository = personRepository;
            _tokenParameters = tokenParameters.Value;
            _config = config;

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
                Token = token
            };

            return authToken;
        }   

        public async Task<AuthToken> SignUpAsync(SignUpDto signUpDto)
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
            };

            await _personRepository.AddPersonAsync(person);

            var token = GenerateToken(person.Login, person.Id);

            AuthToken authToken = new AuthToken
            {
                Token = token,
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
        
    }
}
