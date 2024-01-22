using PersonApi.Entities;
using PersonApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonApi.Repository
{
    public interface IPersonRepository {
        Task AddPersonAsync(Person person);
        Task<List<Person>> GetPeopleAsync();
        Task<Person> GetPersonByIdAsync(long id);
        Task<Person> GetPersonAsync(string login);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(Person person);
        Task AddPhoneVerificationAsync(PhoneVerification phoneVerification);
        Task<PhoneVerification> GetPhoneVerification(long personId);
        Task UpdatePhoneVerification(PhoneVerification phoneVerification);
        Task AddPersonImageAsync(PersonImage image);
        Task<PersonImage> GetPersonImageAsync(long personId);
        Task SaveRefreshTokenAsync(RefreshToken token);
        Task RemoveRefreshTokenAsync(long id);
        Task<Person> GetPersonByRefreshTokenAsync(string refreshToken);
        Task AddRoleChangeRequestAsync(RoleChangeRequest roleChangeRequest);
        Task UpdateRoleChangeRequestAsync(RoleChangeRequest roleChangeRequest);
        Task<RoleChangeRequest> GetRoleChangeRequestAsync(long personId);
        Task<List<RoleChangeRequest>> GetAllPendingRequestsAsync();
    }
}
