namespace BookingSystem.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // Store hashed password
        public string Email { get; set; }
        public string Country { get; set; } // e.g., "SG" for Singapore
    }
}
