using DoctorTrack.Domain.Entities;
using DoctorTrack.Domain.Interfaces;
using DoctorTrack.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrack.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly ICsvExportService _csvExportService;

        public DoctorsController(IDoctorService doctorService,ICsvExportService csvExportService)
        {
            _doctorService = doctorService;
            _csvExportService = csvExportService;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            try
            {
                var doctors = await _doctorService.GetDoctorsAsync();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                 

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{doctorId}/appointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAvailableAppointments(int doctorId)
        {
            try
            {
                var appointments = await _doctorService.GetAvailableAppointmentsAsync(doctorId);
                if (appointments == null || !appointments.Any())
                {
                    return NoContent();
                }
                return Ok(appointments);
            }
            catch (NoSlotsFoundException ex)
            {
               
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost("export-doctors")]
        public async Task<IActionResult> ExportDoctorsToCsv()
        {
            try
            {
                await _csvExportService.ExportDoctorsToCsvAsync();
                return Ok(new { message = "Doctors exported to CSV successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error exporting doctors to CSV: {ex.Message}");
            }
        }


    }
}