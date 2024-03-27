using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorTrack.Domain.DTOs
{
    public class NoSlotsFoundExceptionDto : Exception
    {
        public NoSlotsFoundExceptionDto(string message) : base(message) { }
    }
}
