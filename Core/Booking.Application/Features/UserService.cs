using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces.Managers;
using Booking.Application.Interfaces.Repositories.MongoDb;
using Booking.Application.Models.MongoDB;
using Booking.Domain.Models.SQLServer;
using MongoDB.Bson;
using Booking.Persistence;
using Booking.Application.Interfaces.Repositories.Factory;
using Booking.Infrastructure;
using Booking.Application.ViewModels.User;
using Booking.Application.RequestModels.User;

namespace Booking.Application.Features
{
    /// <summary>
    /// Implementaion of IUserManager interface for provide 'user' related ops
    /// </summary>
    public class UserService : IUserManager
    {
        private readonly IMongoRepository<User> _userRepository;

        public UserService(IMongoRepositoryFactory mongoRepositoryFactory)
        {
            _userRepository = mongoRepositoryFactory.GetRepo<User>();
        }

        public async Task<bool> CreateUserAsync(CreateUserRequest request)
        {
            var user = new User()
            {
                 Email = request.Email,
                 FirstName = request.FirstName,
                 LastName = request.LastName,
                 Password = request.Password
            };

            user.Password = PasswordEncrypter.Encrypt(request.Password);

            var result = await _userRepository.InsertOneAsync(user);
            return result;

        }

        public async Task<List<UserViewModel>> GetAllUsersAsync()
        {
            var result = await _userRepository.GetAllAsync();

            var list = result.Select(i => new UserViewModel()
            {
                Id = i.Id,
                Email = i.Email,
                FirstName = i.FirstName,
                LastName = i.LastName
            }).ToList();
            
            return list;
        }
    }
}
