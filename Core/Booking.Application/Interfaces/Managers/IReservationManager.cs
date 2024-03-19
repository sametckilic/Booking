using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Models.MongoDB;
using Booking.Application.RequestModels.Reservation;
using Booking.Application.ViewModels.Reservation;

namespace Booking.Application.Interfaces.Managers
{
    /// <summary>
    /// Interaface for managing 'Reservation' related ops
    /// </summary>
    public interface IReservationManager
    {
        // asynchronusly returns a list of all reservation
        Task<List<ReservationViewModel>> GetAllAsync();

        //asynchronously deletes a Reservation in the system
        Task<bool> DeleteReservationAsync(DeleteReservationRequest request);

        //asynchronously updates a reservation in the system
        Task<ReservationViewModel> UpdateReservation(UpdateReservationRequest reservation);
    }
}
