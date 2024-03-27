using Newtonsoft.Json;

namespace DoctorTrack.Domain.DTOs
{
    public class ScheduleResponseDto
    {
        [JsonProperty("doctorId")]
        public int DoctorId { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("endTime")]
        public string EndTime { get; set; }

        [JsonProperty("visitId")]
        public int VisitId { get; set; }
    }
}