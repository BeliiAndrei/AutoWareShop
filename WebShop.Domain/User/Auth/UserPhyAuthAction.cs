using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.User.Auth
{
    public class UserPhyAuthAction
    {
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RePassword { get; set; }
        public string PocketPhoneNumber { get; set; }
        public string StationPhoneNumber { get; set; }
        public string City { get; set; }
        public DateTime LoginTime { get; set; }




    }
}
