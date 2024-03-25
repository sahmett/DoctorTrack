using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DoctorTrack.Domain.Entities;
using DoctorTrack.Domain.Interfaces;

namespace DoctorTrack.WebAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://a93ced42-c421-4f38-a0ee-25fc667483c0.mock.pstmn.io/";

        public AppointmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> BookAppointmentAsync(Appointment appointment)
        {
            var requestUri = new Uri($"{BaseUrl}bookVisit");
            var payload = new
            {
                VisitId = appointment.VisitId,
                StartTime = appointment.StartTime.ToString("HH:mm"),
                EndTime = appointment.EndTime.ToString("HH:mm"),
                Date = appointment.StartTime.ToString("dd/MM/yyyy"),
                PatientName = appointment.PatientName,
                PatientSurname = appointment.PatientSurname,
                HospitalId = appointment.HospitalId,
                DoctorId = appointment.DoctorId,
                BranchId = (int)Math.Round((double)appointment.BranchId)
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUri, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to book appointment: {errorContent}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var bookingResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

            if (bookingResponse.status == true && bookingResponse.BookingID != null)
            {
                return bookingResponse.BookingID;
            }

            throw new Exception("Invalid response received from the booking service.");
        }

        public Task<bool> CancelAppointmentAsync(int bookingId)
        {
            throw new NotImplementedException();
        }

        // Implement CancelAppointmentAsync ve diğer metodlar...
    }
}
