namespace BookingSystem.Models
{
    public class Waitlist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public int PositionInQueue { get; set; }
    }
}
