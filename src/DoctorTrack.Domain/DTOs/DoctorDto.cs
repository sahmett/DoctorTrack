using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorTrack.Domain.DTOs
{
    public class DoctorDto
    {
            public string createdAt { get; set; }
            public string name { get; set; }
            public string gender { get; set; }
            public string hospitalName { get; set; }
            public int hospitalId { get; set; }
            public int specialtyId { get; set; }
            public double branchId { get; set; }
            public string nationality { get; set; }
            public string doctorId { get; set; }
   
    }
}
