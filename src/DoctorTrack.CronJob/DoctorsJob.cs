using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorTrack.CronJob
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::DoctorTrack.Domain.Interfaces;
    using global::DoctorTrack.Domain.Entities;

    namespace DoctorTrack.CronJob.Jobs
    {
        public class DoctorsJob
        {
            private readonly IDoctorService _doctorService; // Bu, `DoctorService` sınıfınızın bir örneğidir.

            public DoctorsJob(IDoctorService doctorService)
            {
                _doctorService = doctorService;
            }

            public async Task PrintDoctorsToConsole()
            {
                IEnumerable<Doctor> doctors = await _doctorService.GetDoctorsAsync();

                if (doctors != null && doctors.Any())
                {
                    foreach (var doctor in doctors)
                    {
                        // Print doctor information to the console.
                        System.Console.WriteLine($"Doctor ID: {doctor.Id}, Name: {doctor.Name}");
                    }
                }
                else
                {
                    // If the doctors list is empty or null.
                    System.Console.WriteLine("No doctors found.");
                }
            }

        }
    }
}
