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
                if (bookingId > 0)
                {
                    return Ok(new { BookingID = bookingId });
                }
                else
                {
                    return BadRequest("Randevu oluşturulamadı.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Randevu servisi hatası: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Beklenmeyen bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost("book2")]
        public async Task<ActionResult> BookAppointment2([FromBody] Appointment appointment)
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
                    return Ok(new { status = true });
                else
                    return NotFound(new { status = false, message = "Booking not found" });
            }
            catch (Exception ex)
            {
             return StatusCode(StatusCodes.Status500InternalServerError, "Error booking the appointment");
        }
        }

        [HttpPost("cancel2/{bookingId}")]
        public async Task<ActionResult> CancelAppointment2(int bookingId)
        {
            try
            {
                var result = await _appointmentService.CancelAppointmentAsync(bookingId);
                if (result)
                    return Ok(new { status = true, message = $"Booking with ID {bookingId} was successfully cancelled." });
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

