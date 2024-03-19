using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Models.MongoDB;
using Booking.Application.RequestModels.User;
using Booking.Application.ViewModels.User;

namespace Booking.Application.Interfaces.Managers
{
    /// <summary>
    /// Interface for managing `user` related operations
    /// </summary>
    public interface IUserManager
    {
        // asynchronusly create a new user in the system
        Task<bool> CreateUserAsync(CreateUserRequest request);

        // asynchronusly returns a list of all users form the database
        Task<List<UserViewModel>> GetAllUsersAsync();
    }
}
