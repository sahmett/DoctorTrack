using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorTrack.Domain.Entities
{
    public class Appointment
    {
        public int doctorId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public DateTime date { get; set; }

        /*
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Date { get; set; } 
        */
        public int VisitId { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public int hospitalId { get; set; }
        public double branchId { get; set; }
       
    }


}
