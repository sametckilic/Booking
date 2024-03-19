using Booking.Application.Interfaces.Managers;
using Booking.Application.RequestModels.User;
using Booking.Application.ViewModels.User;
using Booking.Infrastructure.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebAPI.Controllers
{

    /// <summary>
    /// Controller class for handle 'Auth' related endpoints
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager authManager;

        public AuthController(IAuthManager authManager)
        {
            this.authManager = authManager;
        }


        // asynchronously handels login ops for a user based on provided request data.
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            var result = await authManager.Login(request);

            var response = ResponseModel<LoginUserViewModel>.Success(result, "User logged in successfully!");

            return Ok(response);
        }

    }
}
