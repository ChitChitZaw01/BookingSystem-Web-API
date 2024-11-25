using BookingSystem.Models;

namespace BookingSystem.ViewModels
{
    public class BookingVM
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign Key to User
        public int PackageId { get; set; } // Foreign Key to Package
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } // Status (e.g., "Booked", "Completed")

        public User User { get; set; }
        public Package Package { get; set; }
    }
}
