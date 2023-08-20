using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonService.Entities;
using PersonService.Entities.Settings;

namespace PersonService.Repository
{
    public class PersonDbContext : DbContext
    {
        private readonly ConnectionStrings _connectionStrings;

        public PersonDbContext(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }

        public DbSet<Person> People { get; set; }

        public PersonDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStrings.DbConnectionString);
        }
    }
}
