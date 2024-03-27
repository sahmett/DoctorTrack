using Newtonsoft.Json;

namespace DoctorTrack.Domain.DTOs
{
    public class DoctorResponseDto
    {
        [JsonProperty("doctorId")]
        public string DoctorId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("branchId")]
        public double BranchId { get; set; }

    }
}