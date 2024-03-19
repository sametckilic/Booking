using Booking.Application.Interfaces.Managers;
using Booking.Application.ViewModels.Billing;
using Booking.Infrastructure.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingManager billingManager;

        public BillingController(IBillingManager billingManager)
        {
            this.billingManager = billingManager;
        }

        [HttpGet]
        [Route("GetAllBillingPayments")]
        public async Task<IActionResult> GetAllBillingPayments()
        {
            var result = await billingManager.GetAllBillingPayments();

            var response = ResponseModel<List<BillingPaymentsViewModel>>.Success(result, "All bills listed succesfully");

            return Ok(response);
        }

        [HttpGet]
        [Route("GetBillingPaymentById")]
        public async Task<IActionResult> GetBillingPaymentById(Guid id)
        {
            var result = await billingManager.GetBillingPaymentById(id);

            var response = ResponseModel<BillingPaymentsViewModel>.Success(result, "The bill is returned by given Id");

            return Ok(response);
        }

        [HttpGet]
        [Route("GetBillingPaymentsByHotelId")]
        public async Task<IActionResult> GetBillingPaymentsByHotelId(Guid hotelId)
        {
            var result = await billingManager.GetBillingPaymentsByHotelId(hotelId);

            var response = ResponseModel<List<BillingPaymentsViewModel>>.Success(result, "All bills listed succesfully filtred hotelId");

            return Ok(response);
        }

        [HttpGet]
        [Route("GetBillingPaymentsByUserId")]
        public async Task<IActionResult> GetBillingPaymentsByUserId(string userId)
        {
            var result = await billingManager.GetBillingPaymentsByUserId(userId);

            var response = ResponseModel<List<BillingPaymentsViewModel>>.Success(result, "All bills listed succesfully by filtred UserId");

            return Ok(response);
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteBillingPaymentsById")]
        public async Task<IActionResult> DeleteBillingPaymentsById(Guid id)
        {
            var result = await billingManager.DeleteBillingPayment(id);

            var response = ResponseModel<bool>.Success(result, $"The bill {id} has ben deleted from system.");

            return Ok(response);  
        }
    }
}
