using BookingSystem.Data;
using BookingSystem.Models;
using BookingSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Book a package
        [HttpPost("book")]
        [Authorize] // Only authenticated users can book
        public async Task<ActionResult> BookPackage([FromBody] PackageVM request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var package = await _context.Packages
                .FirstOrDefaultAsync(p => p.Id == request.Id);
            if (package == null)
            {
                return NotFound("Package not found");
            }

            var booking = new Booking
            {
                UserId = userId,
                PackageId = package.Id,
                BookingDate = DateTime.Now,
                Status = "Booked"
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Package booked successfully" });
        }

        // Mock AddPaymentCard method
        [HttpPost("addPaymentCard")]
        public bool AddPaymentCard(string cardNumber, string cardHolderName)
        {
            try
            {
                // Simulate a success case
                if (string.IsNullOrWhiteSpace(cardNumber) || string.IsNullOrWhiteSpace(cardHolderName))
                {
                    throw new ArgumentException("Invalid card details provided.");
                }

                // Simulate success
                Console.WriteLine("Payment card added successfully.");
                return true;
            }
            catch (Exception ex)
            {
                // Handle failure case (log, rethrow, or return false)
                Console.WriteLine($"Error adding payment card: {ex.Message}");
                return false;
            }
        }

        // Mock PaymentCharge method
        [HttpPost("paymentCharge")]
        public bool PaymentCharge(string cardNumber, decimal amount)
        {
            try
            {
                // Simulate a charge failure for certain card numbers or amounts
                if (amount <= 0)
                {
                    throw new ArgumentException("Invalid charge amount.");
                }

                if (cardNumber == "0000")  // Mock a card that always fails
                {
                    throw new InvalidOperationException("Payment failed due to card error.");
                }

                // Simulate success
                Console.WriteLine($"Payment of {amount:C} charged successfully.");
                return true;
            }
            catch (Exception ex)
            {
                // Handle failure case (log, rethrow, or return false)
                Console.WriteLine($"Error charging payment: {ex.Message}");
                return false;
            }
        }

        // Mock SendVerifyEmail method
        [HttpPost("sendVerifyEmail")]
        public bool SendVerifyEmail(string emailAddress)
        {
            try
            {
                // Simulate success and failure cases based on the email format
                if (string.IsNullOrWhiteSpace(emailAddress) || !emailAddress.Contains("@"))
                {
                    throw new ArgumentException("Invalid email address.");
                }

                // Simulate success
                Console.WriteLine($"Verification email sent to {emailAddress}.");
                return true;
            }
            catch (Exception ex)
            {
                // Handle failure case (log, rethrow, or return false)
                Console.WriteLine($"Error sending verification email: {ex.Message}");
                return false;
            }
        }
    }
}