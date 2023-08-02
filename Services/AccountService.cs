using Entities;
using Entities.Exceptions;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<Person> _genericRepository;

        public AccountService(IGenericRepository<Person> genericRepository)
        {
            _genericRepository = genericRepository;
        }
            
        public async Task<Person> LogInAsync(string login, string password)
        {
            var person = await (await _genericRepository.QueryAsync()).FirstOrDefaultAsync(x => x.Login == login);

            if (person == null | !BCrypt.Net.BCrypt.Verify(password, person.Password))
            {
                throw new CredentialsExc("Invalid username or password");
            }

            Console.WriteLine("Success!");
            return person;
        }   

        public async Task<Person> SignUpAsync(string login, string password, string email, string country)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var existingPerson = await (await _genericRepository.QueryAsync()).FirstOrDefaultAsync(x => x.Login == login);

            if (existingPerson != null) {
                throw new CredentialsExc("The username is already taken!");
            }

            Person person = new Person
            {
                Login = login,
                Password = passwordHash,
                Email = email,
                Country = country,
            };

            await _genericRepository.CreateAsync(person);

            return person;
        }
    }
}
