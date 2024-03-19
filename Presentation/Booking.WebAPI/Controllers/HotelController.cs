using Booking.Application.Interfaces.Managers;
using Booking.Application.Models.SQLServer;
using Booking.Application.RequestModels.Hotel;
using Booking.Application.ViewModels.Hotel;
using Booking.Infrastructure.Enums;
using Booking.Infrastructure.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelManager hotelManager;

        public HotelController(IHotelManager hotelManager)
        {
            this.hotelManager = hotelManager;
        }

        // Returns a list of hotels in the system
        [HttpGet]
        [Route("GetHotels")]
        public async Task<IActionResult> GetHotels()
        {
            var result = await hotelManager.GetHotels();

            var response = ResponseModel<List<HotelViewModel>>.Success(result, "The Hotels listed successfully");

            return Ok(response);
        }

        // Returns hotel's rooms and prices in the system
        [HttpGet]
        [Route("GetHotelRooms")]
        public async Task<IActionResult> GetHotelRooms(Guid hotelId)
        {
            var result =  await hotelManager.GetHotelRooms(hotelId);

            var response = ResponseModel<HotelRoomsViewModel>.Success(result);

            return Ok(response);
        }

        // Checks the room is avaliable for reservaion
        [HttpGet]
        [Route("IsRoomAvailable")]
        public async Task<IActionResult> IsRoomAvailable(IsRoomAvaliableRequestModel request)
        {
            var result = await hotelManager.IsHotelRoomsAvailableByDates(request);

            var response = ResponseModel<bool>.Success(result, $"{request.RoomId} is available!");

            return Ok(response);
        }


        // Creates a hotel in the system using the provided hotel data.
        [HttpPost]
        [Route("CreateHotel")]
        public async Task<IActionResult> CreateHotel(CreateHotelRequestModel request)
        {
            var result = await hotelManager.CreateHotel(request);
            
            var response = ResponseModel<HotelViewModel>.Success(result, "The hotel created successfully!");

            return Ok(response);
        }

        // Creates a hotel room in the system using provided datas
        [HttpPost]
        [Route("Rooms/CreateRoom")]
        public async Task<IActionResult> CreateHotelRoom(CreateRoomRequestModel request)
        {
            var result = await hotelManager.CreateRoom(request);

            var response = ResponseModel<RoomsViewModel>.Success(result, $"The room created succesfully!");

            return Ok(response);
        }


        // Creates a reservation in the system using provided datas
        [HttpPost]
        [Route("CreateReservation")]
        public async Task<IActionResult> CreateReservation(CreateReservationRequestModel request)
        {
            var result = await hotelManager.CreateReservation(request);

            var response = ResponseModel<bool>.Success(result, "The reservation created succesfully!");

            return Ok(response);
        }


        [HttpPut]
        [Route("UpdateHotel")]
        public async Task<IActionResult> UpdateHotel(UpdateHotelRequestModel request)
        {
            var result = await hotelManager.UpdateHotel(request);

            var response = ResponseModel<HotelViewModel>.Success(result, "The room updated succesfully");

            return Ok(response);
        }


        [HttpDelete]
        [Route("DeleteHotel")]
        public async Task<IActionResult> DeleteHotel(DeleteHotelRequestModel request)
        {
            var result = await hotelManager.DeleteHotel(request);

            var response = ResponseModel<bool>.Success(result, "The hotel deleted sucesfully!");

            return Ok(result);
        }
    }
}
