namespace BookingSystem.ViewModels
{
    public class PackageVM
    {
        public int Id { get; set; }
        public string Name { get; set; } // e.g., "Basic Package"
        public int Credits { get; set; } // Number of credits
        public decimal Price { get; set; } // Price of the package
        public DateTime ExpiryDate { get; set; } // Expiry of the package
        public string Country { get; set; } // The country this package is available for
    }
}
