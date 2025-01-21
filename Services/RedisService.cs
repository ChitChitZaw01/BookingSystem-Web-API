using BookingSystem.Models;
using StackExchange.Redis;
using System.Security.Claims;

namespace BookingSystem.Services
{
    public class RedisService                       
    {
        private readonly IDatabase _database;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private const string BookingKey = "active_bookings";
        private const int MaxBookings = 5;

        //private static ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("localhost");
        //private static IDatabase db = connection.GetDatabase();

        // Constructor accepts IConnectionMultiplexer which is injected
        public RedisService(IConnectionMultiplexer connectionMultiplexer)
        {
            try
            {
                _connectionMultiplexer = connectionMultiplexer;
                _database = _connectionMultiplexer.GetDatabase();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error connecting to Redis: {ex.Message}");
            }
        }

        public async Task<bool> TryBookClassAsync (int userId)
        {

            string activeUsersKey = "active_users";  // The key to track active users

            // Check if the user can book the class (if active users < 5)
            if (await CanAccessAsync(activeUsersKey))
            {
                // If allowed, add the user to the active set
                await _database.SetAddAsync(activeUsersKey, userId);
                Console.WriteLine($"{userId} has accessed the system.");
                return true;
            }
            else
            {
                // If the limit is reached, deny access
                Console.WriteLine("Too many users are active. Please try again later.");
                return false;
            }
        }
        private async Task<bool> CanAccessAsync(string activeUsersKey)
        {
            // Get the number of active users
            long activeUserCount = await _database.SetLengthAsync(activeUsersKey);

            // Allow access if there are fewer than 5 active users
            return activeUserCount < 5;
        }

        // Check if the user can access the system
        static bool CanAccess(IDatabase db, string activeUsersKey)
        {
            // Get the number of active users
            long activeUserCount = db.SetLength(activeUsersKey);

            // Allow access if there are fewer than 5 active users
            return activeUserCount < 5;
        }
        public async Task ResetBookingCountAsync(int userId)
        {
            // Remove the user from the active users set in Redis
            await _database.SetRemoveAsync("active_users", userId);
        }
        public void ResetBookingCount(int userId)
        {
            // Connect to Redis
            var connection = ConnectionMultiplexer.Connect("localhost");
            var db2 = connection.GetDatabase();
            db2.SetRemove("active_users", userId);
            // Close connection
            connection.Close();
        }


    }
}
