using Booking.Application.Interfaces.Managers;
using Booking.Application.Models.MongoDB;
using Booking.Application.RequestModels.Reservation;
using Booking.Application.ViewModels.Reservation;
using Booking.Infrastructure.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationManager reservationManager;

        public ReservationController(IReservationManager reservationManager)
        {
            this.reservationManager = reservationManager;
        }

        // asynchronusly returns a list of all reservations stored in collections
        [HttpGet]
        [Route("GetReservations")]
        public async Task<IActionResult> GetAllReservations() 
        {
            var result = await reservationManager.GetAllAsync();

            var response = ResponseModel<List<ReservationViewModel>>.Success(result, "The reservations listed successfully.");

            return Ok(response);
        }

        // asynchronously deletes reservation stored in collection
        [Authorize]
       [HttpDelete]
       [Route("DeleteReservation")]
        public async Task<IActionResult> DeleteReservation(DeleteReservationRequest request)
        {
            var result = await reservationManager.DeleteReservationAsync(request);

            var response = ResponseModel<bool>.Success(result, "The reservation deleted succesfully.");

            return Ok(response);
        }

        // asynchronously updates reservation stored in collection
        [Authorize]
        [HttpPut]
        [Route("UpdateReservation")]
        public async Task<IActionResult> UpdateReservation(UpdateReservationRequest request)
        {
            var result = await reservationManager.UpdateReservation(request);

            var response = ResponseModel<ReservationViewModel>.Success(result, "The reservation updated succesfully!");

            return Ok(response);
        }
    }
}
