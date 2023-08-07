using Entities;
using Entities.DTO;
using Entities.Exceptions;
using Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<Person> _personRepository;
        private readonly TokenParameters _tokenParameters;

        public AccountService(IGenericRepository<Person> personRepository, IOptions<TokenParameters> tokenParameters)
        {
            _personRepository = personRepository;
            _tokenParameters = tokenParameters.Value;
        }

        public const int LIFETIME = 100;

        public async Task<ResponseDTO> LogInAsync(string login, string password)
        {
            var identity = await GetIdentityAsync(login, password);

            if (identity == null)
            {
                throw new CredentialsExc("Invalid username or password");
            }

            var now = DateTime.UtcNow;

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenParameters.IssuerSigningKey));

            var jwt = new JwtSecurityToken(
                    issuer: _tokenParameters.ValidIssuer,
                    audience: _tokenParameters.ValidAudience,
                    claims: identity.Claims,
                    notBefore: now, 
                    expires: now.Add(TimeSpan.FromMinutes(LIFETIME)),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new ResponseDTO
            {
                Token = encodedJwt,
                Login = identity.Name,
                Country = identity.FindFirst(x => x.Type == ClaimTypes.Locality).Value,
                Email = identity.FindFirst(x => x.Type == ClaimTypes.Email).Value,
            };

            return response;
        }   

        public async Task<Person> SignUpAsync(string login, string password, string email, string country)
        {
            var existingPerson = await (await _personRepository.QueryAsync()).AnyAsync(x => 
            x.Login.Trim().ToUpper() == login.Trim().ToUpper());
                
            if (existingPerson) {
                throw new CredentialsExc("The username is already taken!");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            Person person = new Person
            {
                Login = login,
                Password = passwordHash,
                Email = email,
                Country = country,
            };

            await _personRepository.CreateAsync(person);

            await this.LogInAsync(login, password);

            return person;
        }

        public async Task<ClaimsIdentity> GetIdentityAsync(string login, string password)
        {
            var person = await (await _personRepository.QueryAsync()).FirstOrDefaultAsync(x =>
            x.Login.Trim().ToUpper() == login.Trim().ToUpper());

            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimTypes.Email, person.Email),
                    new Claim(ClaimTypes.Locality, person.Country)
                };
                
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, 
                    ClaimsIdentity.DefaultRoleClaimType);
                
                return claimsIdentity;
            }

            return null;
        }
    }
}
