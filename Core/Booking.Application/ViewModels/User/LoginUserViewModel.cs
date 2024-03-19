using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ViewModels.User
{
    // A viewmodel for login op
    public class LoginUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string AccessToken {  get; set; }
        public DateTime ExpiryDate { get; set; }


    }
}
