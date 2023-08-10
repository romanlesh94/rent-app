﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using rent.Entities;
using rent.Entities.Settings;

namespace rent.Repository
{
    public class ApplicationContext : DbContext
    {
        private readonly ConnectionStrings _connectionStrings;

        public ApplicationContext(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }

        public DbSet<Person> People { get; set; }

        public ApplicationContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStrings.DbConnectionString);
        }
    }
}
