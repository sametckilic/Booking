using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Models
{
    /// <summary>
    /// Represents Token settings for JWT
    /// </summary>
    public class Token
    {
        public string AccessToken {  get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
