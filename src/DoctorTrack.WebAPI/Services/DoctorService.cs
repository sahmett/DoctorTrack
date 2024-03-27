using DoctorTrack.Domain.DTOs;
using DoctorTrack.Domain.Entities;
using DoctorTrack.Domain.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;



namespace DoctorTrack.WebAPI.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress = "https://a93ced42-c421-4f38-a0ee-25fc667483c0.mock.pstmn.io";

        public DoctorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Appointment>> GetAvailableAppointmentsAsync(int doctorId)
        {
            var response = await _httpClient.GetAsync($"{_baseAddress}/fetchSchedules?doctorId={doctorId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var schedulesList = JsonConvert.DeserializeObject<DoctorScheduleListResponseDto>(content)?.Data;
                return schedulesList.Select(s => new Appointment
                {
                    doctorId = s.DoctorId,
                    startTime = DateTime.Parse(s.StartTime),
                    endTime = DateTime.Parse(s.EndTime),
                    VisitId = s.VisitId
                   
                });
            }

            //hata yönetimi
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<ErrorDto>(content);
                // NO_SLOT_FOUND durumu
                if (errorResponse?.Message == "NO_SLOT_FOUND")
                {
                    //özel bir hata fırlat
                    throw new NoSlotsFoundExceptionDto("Müsait randevu zamanı bulunamadı.");
                }

                return new List<Appointment>();
            }
            else
            {
                throw new HttpRequestException("Unable to fetch schedules");
            }
        }


        public async Task<IEnumerable<Doctor>> GetDoctorsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseAddress}/fetchDoctors");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var doctorsList = JsonConvert.DeserializeObject<DoctorListResponseDto>(content)?.Data;
                return doctorsList.Select(d => new Doctor
                {
                    Id = int.Parse(d.DoctorId),
                    Name = d.Name,
                    BranchId = Convert.ToInt32(d.BranchId)
                    
                });
            }
            else
            {
               
                throw new HttpRequestException("Unable to fetch doctors");
            }
        }

    }

}
