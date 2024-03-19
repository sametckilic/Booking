using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Booking.Infrastructure.Models;
using Microsoft.Extensions.Configuration;

namespace Booking.Application.Interfaces
{
    /// <summary>
    /// Interface for handling token related ops in the application.
    /// </summary>
    public interface ITokenHandler
    {
        // creates token in the system by claims
        Token CreateToken(Claim[] claims);
    }
}
