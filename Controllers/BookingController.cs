using BookingSystem.Data;
using BookingSystem.Interfaces;
using BookingSystem.Models;
using BookingSystem.Services;
using BookingSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Security.Claims;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        #region
        private readonly ApplicationDbContext _context;
        private readonly RedisService _redisService;
        public BookingController(RedisService redisService, ApplicationDbContext context)
        {
            _redisService = redisService;
            _context = context;
        }

        //[HttpPost("book/{classId}")]
        //public IActionResult BookClass(string classId)
        //{
        //    bool bookingSuccessful = _redisService.TryBookClass(classId);

        //    if (bookingSuccessful)
        //    {
        //        return Ok(new { message = "Booking successful!" });
        //    }
        //    else
        //    {
        //        return BadRequest(new { message = "Class is already fully booked!" });
        //    }
        //}
        #endregion
        //**************stop Redis

        [HttpPost("book/{packageId}")]
        //[Authorize] // Only authenticated users can book
        public async Task<ActionResult> BookPackage(int packageId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            bool isBooked = await _redisService.TryBookClassAsync(userId);
            if (isBooked)
            {
                var package = await _context.Packages
                    .FirstOrDefaultAsync(p => p.Id == packageId);
                if (package == null)
                {
                    return NotFound("Package not found");
                }

                var booking = new Booking
                {
                    UserId = userId,
                    PackageId = packageId,
                    BookingDate = DateTime.Now,
                    Status = "Booked"
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                await _redisService.ResetBookingCountAsync(userId);  // Reset user booking count
                Console.WriteLine($"{userId} has finsished the system.");

                return Ok(new { message = "Package booked successfully" });
            }
            else
            {
                return BadRequest(new { message = "Class is already fully booked!" });
            }
        }

        #region Mock Functions
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
        #endregion
    }
}