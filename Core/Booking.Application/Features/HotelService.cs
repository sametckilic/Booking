using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Booking.Application.Caching;
using Booking.Application.Interfaces.Managers;
using Booking.Application.Interfaces.Queue;
using Booking.Application.Interfaces.Repositories.EntityFramework;
using Booking.Application.Interfaces.Repositories.Factory;
using Booking.Application.Interfaces.Repositories.MongoDb;
using Booking.Application.Models.MongoDB;
using Booking.Application.Models.SQLServer;
using Booking.Application.RequestModels.Hotel;
using Booking.Application.ViewModels.Hotel;
using Booking.Domain.Models.SQLServer;
using Booking.Infrastructure.Enums;
using Booking.Infrastructure.Events;
using Booking.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Features
{
    /// <summary>
    /// Implementaion of IHotelManager interface for provide 'Hotel' related ops
    /// </summary>
    public class HotelService : IHotelManager
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IHotelRoomRepository hotelRoomRepository;
        private readonly IHotelRoomPriceRepository hotelRoomPriceRepository;
        private readonly IBillingPaymentRepository billingPaymentRepository;
        private readonly IMongoRepository<Reservation> reservationRepository;
        private readonly IMongoRepository<User> userRepository;
        private readonly ICacheService cacheService;
        private readonly IRabbitmqFactory rabbitmqFactory;

        public HotelService(IHotelRepository hotelRepository, IHotelRoomRepository hotelRoomRepository, IHotelRoomPriceRepository hotelRoomPriceRepository, IBillingPaymentRepository billingPaymentRepository, IMongoRepositoryFactory mongoRepositoryFactory, ICacheService cacheService, IRabbitmqFactory rabbitmqFactory)
        {
            this.hotelRepository = hotelRepository;
            this.hotelRoomRepository = hotelRoomRepository;
            this.hotelRoomPriceRepository = hotelRoomPriceRepository;
            this.billingPaymentRepository = billingPaymentRepository;
            this.reservationRepository = mongoRepositoryFactory.GetRepo<Reservation>();
            this.userRepository = mongoRepositoryFactory.GetRepo<User>();
            this.cacheService = cacheService;
            this.rabbitmqFactory = rabbitmqFactory;
        }

        public async Task<bool> IsHotelRoomsAvailableByDates(IsRoomAvaliableRequestModel request)
        {
            var existRoom = await hotelRoomRepository.GetByIdAsync(request.RoomId);

            if (existRoom == null)
                throw new DatabaseValidationException("The room not found!");
            
            var existReservation = (await reservationRepository.GetAllAsync()).ToList();

            existReservation = existReservation.Where(i => i.CheckOutDate > request.CheckInDate && i.CheckInDate < request.CheckOutDate).ToList();
            
            existReservation = existReservation.Where(i => i.RoomId == request.RoomId).ToList();

            return !existReservation.Any();
        }

        public async Task<HotelViewModel> CreateHotel(CreateHotelRequestModel request)
        {
            var existHotel = await hotelRepository.FirstOrDefaultAsync(i => i.HotelName == request.HotelName);

            if (existHotel != null)
                throw new DatabaseValidationException("This hotel is aldready exists!");

            var hotel = new Hotel
            {
                HotelName = request.HotelName
            };

            var result = await hotelRepository.AddAsync(hotel);

            var hotelView = new HotelViewModel()
            {
                Id = hotel.Id,
                HotelName = request.HotelName,
            };

            return hotelView;
        }

        public async Task<bool> CreateReservation(CreateReservationRequestModel request)
        {
            var room = await hotelRoomRepository.FirstOrDefaultAsync(x => x.Id == request.RoomId);

            if (room == null)
                throw new DatabaseValidationException("The room not found!");


            var isRoomAvailable = await IsHotelRoomsAvailableByDates(new IsRoomAvaliableRequestModel(request.RoomId, request.CheckInDate, request.CheckOutDate));

            if (!isRoomAvailable)
                throw new DatabaseValidationException("The hotel room is not available!");

            var user = await userRepository.FindByIdAsync(request.UserId);

            if (user == null)
                throw new DatabaseValidationException("The user not found!");

            var reservation = new Reservation()
            {
                RoomId = request.RoomId,
                HotelId = room.HotelId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                UserId = request.UserId,
            };

            var result = await reservationRepository.InsertOneAsync(reservation);

            var price = await hotelRoomPriceRepository.FirstOrDefaultAsync(i => i.HotelRoomId == request.RoomId);
            var billingPayment = CreateBill(request.UserId, reservation.Id, room.HotelId, request.CheckInDate, request.CheckOutDate, request.BillingType, price.Price);

            var createdBillingPayment = await billingPaymentRepository.AddAsync(billingPayment);

            var reservationEvent = new ReservationCreatedEvent()
            {
                Id = reservation.Id,
                RoomId= reservation.RoomId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                CreateDate = reservation.CreateDate

            };

            rabbitmqFactory.SendMessage<ReservationCreatedEvent>(message :reservationEvent, exchangeName: "Reservation",queueName :"ReservationQueue");

            return result;
        }

        public async Task<RoomsViewModel> CreateRoom(CreateRoomRequestModel request)
        {
            await cacheService.Clear("GetHotelRooms-"+request.HotelId);

            var existHotel = await hotelRepository.GetByIdAsync(request.HotelId);
            
            if (existHotel == null)
                throw new DatabaseValidationException("The hotel not found!");

            var createdRoom = new HotelRoom()
            {
                HotelId = request.HotelId,
                RoomType = request.RoomType,
            };

            var roomAddedRow = await hotelRoomRepository.AddAsync(createdRoom);

            var createdRoomPrice = new HotelRoomPrice()
            {
                HotelId = createdRoom.HotelId,
                HotelRoomId = createdRoom.Id,
                Price = request.RoomPrice,
                RoomType = request.RoomType
            };

            if (roomAddedRow != 0)
            {
                var roomPriceAddedRow = await hotelRoomPriceRepository.AddAsync(createdRoomPrice);
                if(roomPriceAddedRow != 0)
                {
                    var res = new RoomsViewModel()
                    {
                         RoomId = createdRoom.Id,
                         RoomType= createdRoom.RoomType,
                         RoomPrice = createdRoomPrice.Price
                    };
                    return res;
                }
            }
            throw new DatabaseValidationException("An error occured while creating rooms!");

        }

        public async Task<HotelRoomsViewModel> GetHotelRooms(Guid hotelId)
        {
            var cacheResult = await cacheService.GetOrAddAsync<HotelRoomsViewModel>("GetHotelRooms-" + hotelId, async () =>
            {
                var query = hotelRepository.AsQueryable();

                query = query.Where(i => i.Id == hotelId);

                query = query.Include(i => i.HotelRooms).ThenInclude(i => i.HotelRoomPrices);


                var result = await query.Select(i => new HotelRoomsViewModel()
                {
                    HotelId = hotelId,
                    HotelName = i.HotelName,
                    Rooms = i.HotelRooms.SelectMany(x => x.HotelRoomPrices
                                            .Where(y => y.HotelId == hotelId)
                                            .Select(y => new RoomsViewModel()
                                            {
                                                RoomId = x.Id,
                                                RoomPrice = y.Price,
                                                RoomType = y.RoomType,
                                            })).ToList()
                }).FirstOrDefaultAsync();

                if (result == null)
                    throw new DatabaseValidationException("The hotel or rooms not found!");

                return result;
            });

            return cacheResult;

        }

        public async Task<List<HotelViewModel>> GetHotels()
        {
            var result = await hotelRepository.GetAll();

            var list = result.Select(i => new HotelViewModel()
            {
                HotelName = i.HotelName,
                Id = i.Id,
            }).ToList();

            return list;
        }

        public async Task<HotelViewModel> UpdateHotel(UpdateHotelRequestModel request)
        {
            var existHotel = await hotelRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (existHotel == null)
                throw new DatabaseValidationException("The hotel not found!");

            var hotel = new Hotel()
            {
                Id = request.Id,
                HotelName = request.HotelName,
            };

            var result = await hotelRepository.UpdateAsync(hotel);

            var hotelViewModel = new HotelViewModel()
            {
                Id = hotel.Id,
                HotelName = hotel.HotelName,
            };

            return hotelViewModel;
        }

        public async Task<bool> DeleteHotel(DeleteHotelRequestModel request)
        {
            var existHotel = await hotelRepository.FirstOrDefaultAsync(x => x.Id == request.HotelId);

            if (existHotel == null)
                throw new DatabaseValidationException("The hotel not found!");

            var result = await hotelRepository.DeleteAsync(existHotel);

            return true;
        }




        // creates billingpayment entity
        private BillingPayment CreateBill(string userId, string reservationId, Guid hotelId, DateTime checkInDate, DateTime checkOutDate, BillingType billingType, decimal price)
        {
            var days = (checkOutDate - checkInDate).Days;
            var amount = days * price;

            var billingPayment = new BillingPayment()
            {
                UserId = userId,
                ReservationId = reservationId,
                HotelId = hotelId,
                Amount = amount,
                BillingType = billingType,
                CreateDate = DateTime.UtcNow,
                
            };

            return billingPayment;
        }

    }
}
