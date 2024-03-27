using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorTrack.Domain.DTOs
{
    public class DoctorScheduleListResponseDto
    {
        [JsonProperty("data")]
        public List<ScheduleResponseDto> Data { get; set; }
    }
}
