using StackExchange.Redis;

namespace BookingSystem.Services
{
    public class RedisService
    {
        //private readonly ConnectionMultiplexer _redisConnection;
        private readonly IDatabase _database;

        private readonly IConnectionMultiplexer _connectionMultiplexer;

        // Constructor accepts IConnectionMultiplexer which is injected
        public RedisService(IConnectionMultiplexer connectionMultiplexer)
        {
            //_connectionMultiplexer = connectionMultiplexer;
            //_database = _connectionMultiplexer.GetDatabase();
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

        public bool TryBookClass(string classId)
        {
            var bookingCountKey = $"class:{classId}:bookings";
            var maxBookings = 5;

            // Increment the current booking count atomically, return false if it exceeds max bookings.
            var currentBookings = _database.StringIncrement(bookingCountKey);
            return currentBookings <= maxBookings;
        }

        public int GetCurrentBookingCount(string classId)
        {
            var bookingCountKey = $"class:{classId}:bookings";
            return (int)_database.StringGet(bookingCountKey);
        }

        public void ResetBookingCount(string classId)
        {
            var bookingCountKey = $"class:{classId}:bookings";
            _database.KeyDelete(bookingCountKey);
        }
    }
}
