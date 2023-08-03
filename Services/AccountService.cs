using Entities;
using Entities.Exceptions;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<Person> _personRepository;

        public AccountService(IGenericRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }
            
        public async Task<Person> LogInAsync(string login, string password)
        {
            var person = await (await _personRepository.QueryAsync()).FirstOrDefaultAsync(x => 
            x.Login.Trim().ToUpper() == login.Trim().ToUpper());

            if (person == null | !BCrypt.Net.BCrypt.Verify(password, person.Password))
            {
                throw new CredentialsExc("Invalid username or password");
            }

            Console.WriteLine("Success!");
            return person;
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

            return person;
        }
    }
}
