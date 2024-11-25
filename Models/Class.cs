namespace BookingSystem.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string Country { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RequiredCredits { get; set; }
        public int AvailableSlots { get; set; }
    }
}
