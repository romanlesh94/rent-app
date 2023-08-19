using rent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rent.Repository
{
    public interface IPersonRepository {
        Task AddPersonAsync(Person person);
        Task<List<Person>> GetPeopleAsync();
        Task<Person> GetPersonByIdAsync(long id);
        Task<Person> GetPersonAsync(string login);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(Person person);
    }
}
