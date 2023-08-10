using HouseApi.Models;
using HouseApi.Models.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HouseApi.Repository
{
    public class HouseDbContext : DbContext
    {
        private readonly ConnectionStrings _connectionStrings;
        public HouseDbContext(IOptions<ConnectionStrings> connectionStrings) 
        {
            _connectionStrings = connectionStrings.Value;
        }

        public DbSet<House> Houses { get; set; }

        public HouseDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStrings.DbConnectionString);
        }
    }
}
