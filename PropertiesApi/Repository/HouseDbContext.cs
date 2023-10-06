using HouseApi.Models;
using HouseApi.Models.Booking;
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
        public DbSet<HousePropertyMapping> HousePropertyMappings { get; set; }
        public DbSet<Property> HouseProperties { get; set; }
        public DbSet<HouseBooking> Bookings { get; set; }
        public DbSet<Image> Images { get; set; }

        public HouseDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStrings.DbConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HouseBooking>().Property(b => b.CheckInDate).HasColumnType("date");
            modelBuilder.Entity<HouseBooking>().Property(b => b.CheckOutDate).HasColumnType("date");

            Property property1 = new Property { Id = 1, PropertyText = "Free Wi-Fi" };
            Property property2 = new Property { Id = 2, PropertyText = "King-size bed" };
            Property property3 = new Property { Id = 3, PropertyText = "Free parking" };
            Property property4 = new Property { Id = 4, PropertyText = "Breakfast included" };
            Property property5 = new Property { Id = 5, PropertyText = "Pet-friendly" };
            Property property6 = new Property { Id = 6, PropertyText = "Non-smoking" };
            Property property7 = new Property { Id = 7, PropertyText = "Swimming pool" };
            Property property8 = new Property { Id = 8, PropertyText = "Safe for children" };
            Property property9 = new Property { Id = 9, PropertyText = "Transfer from the airport" };
            Property property10 = new Property { Id = 10, PropertyText = "Beautiful view" };
            Property property11 = new Property { Id = 11, PropertyText = "BBQ" };
            Property property12 = new Property { Id = 12, PropertyText = "Balcony" };
            Property property13 = new Property { Id = 13, PropertyText = "Outside area" };
            Property property14 = new Property { Id = 14, PropertyText = "TV" };
            Property property15 = new Property { Id = 15, PropertyText = "Washing machine" };

            modelBuilder.Entity<Property>().HasData(property1, property2, property3, property4, property5,
                property6, property7, property8, property9, property10, property11, property12, property13, property14, property15);

            HousePropertyMapping housePropertyMapping1 = new HousePropertyMapping { Id = 1, HouseId = 1, PropertyId = 2};
            HousePropertyMapping housePropertyMapping2 = new HousePropertyMapping { Id = 2, HouseId = 1, PropertyId = 3};
            HousePropertyMapping housePropertyMapping3 = new HousePropertyMapping { Id = 3, HouseId = 1, PropertyId = 5};
            HousePropertyMapping housePropertyMapping4 = new HousePropertyMapping { Id = 4, HouseId = 2, PropertyId = 2};
            HousePropertyMapping housePropertyMapping5 = new HousePropertyMapping { Id = 5, HouseId = 2, PropertyId = 13};
            HousePropertyMapping housePropertyMapping6 = new HousePropertyMapping { Id = 6, HouseId = 2, PropertyId = 11};
            HousePropertyMapping housePropertyMapping7 = new HousePropertyMapping { Id = 7, HouseId = 3, PropertyId = 10};
            HousePropertyMapping housePropertyMapping8 = new HousePropertyMapping { Id = 8, HouseId = 3, PropertyId = 9};
            HousePropertyMapping housePropertyMapping9 = new HousePropertyMapping { Id = 9, HouseId = 3, PropertyId = 7};

            modelBuilder.Entity<HousePropertyMapping>().HasData(housePropertyMapping1, housePropertyMapping2, housePropertyMapping3, housePropertyMapping4, housePropertyMapping5, housePropertyMapping6, housePropertyMapping7, housePropertyMapping8, housePropertyMapping9);
        }
    }
}
