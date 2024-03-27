using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DoctorTrack.Domain.Entities;
using DoctorTrack.Domain.Interfaces;
using Microsoft.Extensions.Hosting;

namespace DoctorTrack.WebAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HttpClient _httpClient;
        private const string _baseAddress = "https://a93ced42-c421-4f38-a0ee-25fc667483c0.mock.pstmn.io/";

        public AppointmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<int> BookAppointmentAsync(Appointment appointment)
        {
          
            var query = $"?VisitId={appointment.VisitId}&startTime={appointment.startTime.ToString("HH:mm")}" +
                        $"&endTime={appointment.endTime.ToString("HH:mm")}&date={appointment.date.ToString("dd/MM/yyyy")}" +
                        $"&PatientName={appointment.PatientName}&PatientSurname={appointment.PatientSurname}" +
                        $"&hospitalId={appointment.hospitalId}&doctorId={appointment.doctorId}&branchId={appointment.branchId}";

          
            var requestUri = $"{_baseAddress}bookVisit{query}";

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            
            var response = await _httpClient.SendAsync(request);

           
            //response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var bookingResponse = JsonConvert.DeserializeObject<BookingResponse>(responseContent);

            
            if (bookingResponse.Status && bookingResponse.BookingID.HasValue)
            {
                return bookingResponse.BookingID.Value;
            }

            throw new Exception("Invalid response received from the booking service.");
        }

        public async Task<bool> CancelAppointmentAsync(int bookingId)
        {
           // Ziyareti iptal etmek için POST https://a93ced42-c421-4f38-a0ee-25fc667483c0.mock.pstmn.io/bookVisit?BookingID=133213

            var query = $"?BookingID={bookingId}";

            var requestUri = $"{_baseAddress}cancelVisit{query}";
   
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }
     
        private class BookingResponse
        {
            public bool Status { get; set; }
            public int? BookingID { get; set; }
        }

    }
}
