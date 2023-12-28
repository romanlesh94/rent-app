using Microsoft.EntityFrameworkCore;
using PersonApi.Entities;
using PersonApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonApi.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonDbContext _context;

        public PersonRepository(PersonDbContext context)
        {
            _context = context;
        }

        public async Task AddPersonAsync(Person person)
        {
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person> GetPersonByIdAsync(long id)
        {
            return await _context.People
                .Include(x => x.RefreshToken)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Person> GetPersonAsync(string login)
        {
            return await _context.People
                .Include(x => x.RefreshToken)
                .FirstOrDefaultAsync(p => p.Login.Trim().ToUpper() == login.Trim().ToUpper());
        }

        public async Task UpdatePersonAsync(Person person)
        {
            _context.People.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(Person person)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task AddPhoneVerificationAsync(PhoneVerification phoneVerification)
        {
            await _context.PhoneVerificationTable.AddAsync(phoneVerification);
            await _context.SaveChangesAsync();
        }

        public async Task<PhoneVerification> GetPhoneVerification(long personId)
        {
            return await _context.PhoneVerificationTable.FirstOrDefaultAsync(v => v.PersonId == personId);
        }

        public async Task UpdatePhoneVerification(PhoneVerification phoneVerification)
        {
            _context.PhoneVerificationTable.Update(phoneVerification);
            await _context.SaveChangesAsync();
        }

        public async Task AddPersonImageAsync(PersonImage image)
        {
            await _context.PersonImages.AddAsync(image);

            await _context.SaveChangesAsync();
        }

        public async Task<PersonImage> GetPersonImageAsync(long personId)
        {
            return await _context.PersonImages.FirstOrDefaultAsync(i => i.PersonId == personId);
        }

        public async Task SaveRefreshTokenAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveRefreshTokenAsync(long id)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Id == id);

            if (token != null)
            {
                _context.RefreshTokens.Remove(token);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Person> GetPersonByRefreshTokenAsync(string refreshToken)
        {
            return await _context.People
                .Include(x => x.RefreshToken)
                .FirstOrDefaultAsync(x => x.RefreshToken.Token == refreshToken);
        }
    }
}







