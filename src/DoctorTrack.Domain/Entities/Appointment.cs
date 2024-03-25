using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorTrack.Domain.Entities
{
    public class Appointment
    {
        public int DoctorId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public DateTimeOffset Date { get; set; }
        public int VisitId { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public int HospitalId { get; set; }
        public int BranchId { get; set; }
       
    }


}
