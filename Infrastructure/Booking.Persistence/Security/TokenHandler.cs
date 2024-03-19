using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Interfaces;
using Booking.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Booking.Persistence.Security
{
    /// <summary>
    ///  Class for handling token related ops in the application.
    /// Implements the ITokenHandler interface.
    /// </summary>
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateToken(Claim[] claim)
        {
            Token token = new();

            var key = _configuration["JwtSettings:Key"];

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            token.ExpirationTime = DateTime.UtcNow.AddDays(1);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(claims: claim,expires: token.ExpirationTime, signingCredentials: credentials, notBefore: DateTime.Now);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
