using BookingSystem.Models;
using BookingSystem.ViewModels;
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
        }
    }
}
