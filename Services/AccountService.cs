using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var person = await (await _genericRepository.QueryAsync()).FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

            if (person != null)
            {
                Console.WriteLine("Success!");
                return person;
            }

            return null;
        }

        public async Task<Person> SignUpAsync(string login, string password, string email, string country)
        {
            Person person = new Person
            {
                Login = login,
                Password = password,
                Email = email,
                Country = country,
            };

            await _genericRepository.CreateAsync(person);

            return person;
        }
    }
}
