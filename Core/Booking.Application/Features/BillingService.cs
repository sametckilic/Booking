using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces.Managers;
using Booking.Application.Interfaces.Repositories.EntityFramework;
using Booking.Application.Interfaces.Repositories.Factory;
using Booking.Application.Interfaces.Repositories.MongoDb;
using Booking.Application.Models.MongoDB;
using Booking.Application.Models.SQLServer;
using Booking.Application.ViewModels.Billing;
using Booking.Infrastructure.Exceptions;

namespace Booking.Application.Features
{
    public class BillingService : IBillingManager
    {
        private readonly IBillingPaymentRepository billingPaymentRepository;
        private readonly IMongoRepository<User> userRepository;
        private readonly IHotelRepository hotelRepository;

        public BillingService(IBillingPaymentRepository billingPaymentRepository, IMongoRepositoryFactory mongoRepositoryFactory, IHotelRepository hotelRepository)
        {
            this.billingPaymentRepository = billingPaymentRepository;
            this.userRepository = mongoRepositoryFactory.GetRepo<User>();
            this.hotelRepository = hotelRepository;
        }

        public async Task<bool> DeleteBillingPayment(Guid id)
        {
            var billingPayment = await billingPaymentRepository.GetByIdAsync(id);

            if (billingPayment == null)
                throw new DatabaseValidationException("The billing payment not found!");

            await billingPaymentRepository.DeleteAsync(billingPayment);

            return true;  
        }

        public async Task<List<BillingPaymentsViewModel>> GetAllBillingPayments()
        {
            var result = await billingPaymentRepository.GetAll();

            var list = result.Select(i => new BillingPaymentsViewModel()
            {
                Id = i.Id,
                UserId = i.UserId,
                Amount = i.Amount,
                BillingType = i.BillingType,
                CreateDate = i.CreateDate,
                HotelId = i.HotelId,
                ReservationId = i.ReservationId,
               
            }).ToList();

            return list;
        }

        public async Task<BillingPaymentsViewModel> GetBillingPaymentById(Guid id)
        {
            var result = await billingPaymentRepository.FirstOrDefaultAsync(i => i.Id == id);

            if (result == null)
                throw new DatabaseValidationException("The bills not found!");

            var bill = new BillingPaymentsViewModel()
            {
                Id = result.Id,
                UserId = result.UserId,
                Amount = result.Amount,
                BillingType = result.BillingType,
                CreateDate = result.CreateDate,
                HotelId = result.HotelId,
                ReservationId = result.ReservationId,

            };

            return bill;
        }

        public async Task<List<BillingPaymentsViewModel>> GetBillingPaymentsByHotelId(Guid hotelId)
        {
            var hotel = await hotelRepository.FirstOrDefaultAsync(i => i.Id == hotelId);

            if (hotel == null)
                throw new DatabaseValidationException("The hotel not found!");

            var result = await billingPaymentRepository.GetList(i => i.HotelId == hotelId);

            if (result == null)
                throw new DatabaseValidationException("The bills not found!");

            var list = result.Select(i => new BillingPaymentsViewModel()
            {
                Id = i.Id,
                UserId = i.UserId,
                Amount = i.Amount,
                BillingType = i.BillingType,
                CreateDate = i.CreateDate,
                HotelId = i.HotelId,
                ReservationId = i.ReservationId,

            }).ToList();

            return list;        
        }

        public async Task<List<BillingPaymentsViewModel>> GetBillingPaymentsByUserId(string userId)
        {
            var user = await userRepository.FindByIdAsync(userId);

            if (user == null)
                throw new DatabaseValidationException("The user not found!");

            var result = await billingPaymentRepository.GetList(i => i.UserId == userId);

            if (result == null)
                throw new DatabaseValidationException("The bills not found!");

            var list = result.Select(i => new BillingPaymentsViewModel()
            {
                Id = i.Id,
                UserId = i.UserId,
                Amount = i.Amount,
                BillingType = i.BillingType,
                CreateDate = i.CreateDate,
                HotelId = i.HotelId,
                ReservationId = i.ReservationId,

            }).ToList();

            return list;
        }
    }
}
