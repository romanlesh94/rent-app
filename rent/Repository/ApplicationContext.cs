using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonApi.Entities;
using PersonApi.Entities.Settings;
using PersonApi.Models;

namespace PersonApi.Repository
{
    public class PersonDbContext : DbContext
    {
        private readonly ConnectionStrings _connectionStrings;

        public PersonDbContext(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }

        public DbSet<Person> People { get; set; }
        public DbSet<PhoneVerification> PhoneVerificationTable { get; set; }
        public DbSet<PersonImage> PersonImages { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; } 

        public PersonDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStrings.DbConnectionString);
        }
    }
}
