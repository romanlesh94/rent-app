using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountService
    {
        Task<Person> LogInAsync(string login, string password);
        Task<Person> SignUpAsync(string login, string password, string email, string country);
    }
}
