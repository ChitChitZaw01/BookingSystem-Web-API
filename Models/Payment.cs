namespace BookingSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public int TransactionId { get; set; }
    }
}
