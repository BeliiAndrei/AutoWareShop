using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Auth;

namespace WebShop.Domain.User.Registration
{
    public class UserRegistrationResponse
    {
        public bool Status { get; set; }
        public string StatusMsg { get; set; }
        public UserDBTable User { get; set; }
    }
}
