using BookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "user1", PasswordHash = "$2a$11$2ZAml/2dIg45uH3UkXNFKu9Fvl/PUtSB/t0TT0ecj3/D6ARx/352e", Email = "ccz@gmail.com", Country = "Singapore" },
                new User { Id = 2, Username = "user2", PasswordHash = "$2a$11$2ZAml/2dIg45uH3UkXNFKu9Fvl/PUtSB/t0TT0ecj3/D6ARx/352e", Email = "ccz2@gmail.com", Country = "Singapore" },
                new User { Id = 3, Username = "user3", PasswordHash = "$2a$11$2ZAml/2dIg45uH3UkXNFKu9Fvl/PUtSB/t0TT0ecj3/D6ARx/352e", Email = "ccz@3gmail.com", Country = "Myanmar" }
            );

            ////Configure the 'Price' property explicitly to set the store type(column type)
            //modelBuilder.Entity<Package>()
            //    .Property(p => p.Price)
            //    .HasColumnType("decimal(6,3)");  // Specify the decimal precision and scale
            //modelBuilder.Entity<Package>().HasData(
            //    new Package { Id = 1, Name = "package1", Credits = 12345, Price = 12345.123m, ExpiryDate = DateTime.Now, Country = "Myanmar" },
            //    new Package { Id = 2, Name = "package2", Credits = 12345, Price = 123456.123m, ExpiryDate = DateTime.Now, Country = "Myanmar" },
            //    new Package { Id = 3, Name = "package3", Credits = 12345, Price = 123456.123m, ExpiryDate = DateTime.Now, Country = "Singapore" },
            //    new Package { Id = 4, Name = "package4", Credits = 12345, Price = 123456.123m, ExpiryDate = DateTime.Now, Country = "Singapore" }
            //);
        }
    }
}
