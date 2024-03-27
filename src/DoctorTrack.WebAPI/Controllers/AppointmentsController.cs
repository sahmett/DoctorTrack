using DoctorTrack.Domain.Entities;
using DoctorTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrack.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("book")]
        public async Task<ActionResult> BookAppointment([FromBody] Appointment appointment)
        {
            try
            {
                var bookingId = await _appointmentService.BookAppointmentAsync(appointment);
                return Ok(new { status = true, BookingID = bookingId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error booking the appointment");
            }
        }

        [HttpPost("cancel/{bookingId}")]
        public async Task<ActionResult> CancelAppointment(int bookingId)
        {
            try
            {
                var result = await _appointmentService.CancelAppointmentAsync(bookingId);
                if (result)
                    return Ok(new { status = true, message = $"The reservation with ID {bookingId} if any, was successfully canceled." });
                //$"The reservation with ID {bookingId}, if any, was successfully canceled."
                else
                    // hata mesajı 
                    return NotFound(new { status = false, message = $"Booking with ID {bookingId}" +
                        $" not found. It may have been cancelled already or does not exist." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = false,
                    message = "An error occurred while cancelling the appointment.", error = ex.Message });
            }
        }

       


    }
}

