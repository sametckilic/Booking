using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Models.SQLServer;
using Booking.Application.RequestModels.Hotel;
using Booking.Application.ViewModels.Hotel;
using Booking.Infrastructure.Enums;

namespace Booking.Application.Interfaces.Managers
{
    /// <summary>
    /// Interaface for managing 'Hotel' related ops
    /// </summary>
    public interface IHotelManager
    {
        // asynchronusly creates hotel entity in system
        Task<HotelViewModel> CreateHotel(CreateHotelRequestModel request);
        
        // asynchronously updates hotel entity in system
        Task<HotelViewModel> UpdateHotel(UpdateHotelRequestModel request);

        // asynchronously creates room entity and related roomprice
        Task<RoomsViewModel> CreateRoom(CreateRoomRequestModel request);

        // asynchronously returns a list of all hotels
        Task<List<HotelViewModel>> GetHotels();

        // asynchronously returns a list of rooms with related hotel
        Task<HotelRoomsViewModel> GetHotelRooms(Guid hotelId);
        
        // asynchronously checks room's that has a entry in reservation collection for between given dates
        Task<bool> IsHotelRoomsAvailableByDates(IsRoomAvaliableRequestModel request);

        // asynchronously creates a reservation in reservation collection (uses checksishotelroomavailbebydates)
        Task<bool> CreateReservation(CreateReservationRequestModel request);

        // asynchronusly deletes hotel entity in database
        Task<bool> DeleteHotel(DeleteHotelRequestModel request);
    }
}
