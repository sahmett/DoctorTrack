using DoctorTrack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorTrack.Domain.Interfaces
{
    public interface IAppointmentService
    {
        Task<int> BookAppointmentAsync(Appointment appointment);
        Task<bool> CancelAppointmentAsync(int bookingId);


    }


}
