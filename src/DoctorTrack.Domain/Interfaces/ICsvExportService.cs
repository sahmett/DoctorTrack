using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorTrack.Domain.Interfaces
{
    public interface ICsvExportService
    {
        Task ExportDoctorsToCsvAsync();
    }
}
