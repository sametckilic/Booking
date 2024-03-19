using Booking.Application.Interfaces.Managers;
using Booking.Application.Models.MongoDB;
using Booking.Application.RequestModels.User;
using Booking.Application.ViewModels.User;
using Booking.Infrastructure.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebAPI.Controllers
{

    // Controller class for handle 'user' related endpoints
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }



       // asynchronusly returns a list of all users
       [HttpGet]
       [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await userManager.GetAllUsersAsync();

            var response = ResponseModel<List<UserViewModel>>.Success(result, "All user listed successfully!");

            return Ok(response);
        }


        // asynchronously creates a user and returns boolean
        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var result = await userManager.CreateUserAsync(request);

            var response = ResponseModel<bool>.Success(result, "User created successfully!");

            return Ok(response);
        }
    }
}
