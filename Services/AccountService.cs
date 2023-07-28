using Entities;
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

        public async Task<Person> LogInAsync(string Login, string Password)
        {
            
        }
    }
}
