using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.RequestModels.User;
using Booking.Application.ViewModels.User;

namespace Booking.Application.Interfaces.Managers
{
    /// <summary>
    /// Interface for managing auth related operations in the application.
    /// </summary>
    public interface IAuthManager
    {
        // Handles the login operation for a user based on the provided request data.
        Task<LoginUserViewModel> Login(LoginUserRequest request);
    }
}
