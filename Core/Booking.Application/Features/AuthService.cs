using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces;
using Booking.Application.Interfaces.Managers;
using Booking.Application.Interfaces.Repositories.Factory;
using Booking.Application.Interfaces.Repositories.MongoDb;
using Booking.Application.Models.MongoDB;
using Booking.Application.RequestModels.User;
using Booking.Application.ViewModels.User;
using Booking.Infrastructure;
using Booking.Infrastructure.Exceptions;
using Booking.Infrastructure.Models;

namespace Booking.Application.Features
{
    /// <summary>
    /// Implementaion of IAuthManager interface for provide 'Auth' related ops
    /// </summary>
    public class AuthService : IAuthManager
    {
        private readonly ITokenHandler tokenHandler;
        private readonly IMongoRepository<User> userRepository;

        public AuthService(ITokenHandler tokenHandler, IMongoRepositoryFactory mongoRepositoryFactory)
        {
            this.tokenHandler = tokenHandler;
            this.userRepository = mongoRepositoryFactory.GetRepo<User>();
        }

        public async Task<LoginUserViewModel> Login(LoginUserRequest request)
        {
            var dbUser = await userRepository.FindOneAsync(i => i.Email == request.Email);

            if (dbUser == null)
                throw new DatabaseValidationException("User not found!");

            var pass = PasswordEncrypter.Encrypt(request.Password);

            if (dbUser.Password != pass)
                throw new DatabaseValidationException("Password is wrong!");

            var claims = new Claim[]
            {
                new Claim("id", dbUser.Id.ToString()),
                new Claim("emailAddress", dbUser.Email),
            };

            Token token = tokenHandler.CreateToken(claims);

            var result = new LoginUserViewModel()
            {
                Id = dbUser.Id,
                Email = dbUser.Email,
                AccessToken = token.AccessToken,
                ExpiryDate =  token.ExpirationTime
            };

            return result;

        }
    }
}
