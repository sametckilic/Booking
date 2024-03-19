using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Models.SQLServer;
using Booking.Application.ViewModels.Billing;

namespace Booking.Application.Interfaces.Managers
{
    /// <summary>
    /// Interaface for managing 'Billing' related ops
    /// </summary>
    public interface IBillingManager
    {
        // asynchronusly returns all bills in the system
        Task<List<BillingPaymentsViewModel>> GetAllBillingPayments();

        // asynchronusly returns bill in the system by given id
        Task<BillingPaymentsViewModel> GetBillingPaymentById(Guid id);

        // asynchronusly returns a list of bills in the system of hotel's by given hotelId
        Task<List<BillingPaymentsViewModel>> GetBillingPaymentsByHotelId(Guid hotelId);

        // asynchronusly returns a list of bills in the system of user's by given userId
        Task<List<BillingPaymentsViewModel>> GetBillingPaymentsByUserId(string userId);

        // asynchronusly deletes billingpayments in the system
        Task<bool> DeleteBillingPayment(Guid id);



    }
}
