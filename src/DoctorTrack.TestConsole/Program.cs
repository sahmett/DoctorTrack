using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CancelBookingConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bookingId = "133213";
            var baseUrl = "https://a93ced42-c421-4f38-a0ee-25fc667483c0.mock.pstmn.io";
            var resource = $"/cancelVisit?BookingID={bookingId}";

            using var client = new HttpClient();
            try
            {
                var response = await client.PostAsync(baseUrl + resource, null);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Booking with ID {bookingId} cancelled successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to cancel booking with ID {bookingId}. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
