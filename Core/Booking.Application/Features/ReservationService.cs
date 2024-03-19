using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces.Managers;
using Booking.Application.Interfaces.Repositories.Factory;
using Booking.Application.Interfaces.Repositories.MongoDb;
using Booking.Application.Models.MongoDB;
using Booking.Application.RequestModels.Reservation;
using Booking.Application.ViewModels.Reservation;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Booking.Application.Features
{
    /// <summary>
    /// Implementaion of IReservationManager interface for provide 'Reservartion' related ops
    /// </summary>
    public class ReservationService : IReservationManager
    {
        private readonly IMongoRepository<Reservation> reservationRepository;

        public ReservationService(IMongoRepositoryFactory mongoRepositoryFactory)
        {
            this.reservationRepository = mongoRepositoryFactory.GetRepo<Reservation>();
        }

        public async Task<bool> DeleteReservationAsync(DeleteReservationRequest request)
        {
            await reservationRepository.DeleteOneAsync(x => x.Id == request.ReservationId);

            return true;
        }

        public async Task<List<ReservationViewModel>> GetAllAsync()
        {
                var result = await reservationRepository.GetAllAsync();

                var list = result.Select(i => new ReservationViewModel()
                {
                    Id = i.Id,
                    CheckInDate = i.CheckInDate,
                    CheckOutDate = i.CheckOutDate,
                    CreateDate = i.CreateDate,
                    HotelId = i.HotelId,
                    RoomId = i.RoomId
                
                }).ToList();

                return list;
        }

        public async Task<ReservationViewModel> UpdateReservation(UpdateReservationRequest request)
        {

            var reservation = new Reservation()
            {
                Id = request.Id,
                HotelId = request.HotelId,
                RoomId = request.RoomId,
                UserId = request.UserId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
            };

            await reservationRepository.ReplaceOneAsync(reservation);

            var model = new ReservationViewModel()
            {
                Id = reservation.Id,
                HotelId = reservation.HotelId,
                RoomId = reservation.RoomId,
                UserId = reservation.UserId,
                CreateDate = reservation.CreateDate,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
            };

            return model;
        }
    }
}
