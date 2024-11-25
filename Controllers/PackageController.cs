using BookingSystem.Data;
using BookingSystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PackageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all available packages for the user's country
        [HttpGet("available")]
        [Authorize] // Only authenticated users can access
        public async Task<ActionResult<IEnumerable<Package>>> GetAvailablePackages()
        {
            var userCountry = User.FindFirstValue(ClaimTypes.Country);
            var packages = await _context.Packages
                .Where(p => p.Country == userCountry)
                .ToListAsync();

            return Ok(packages);
        }
    }
}
