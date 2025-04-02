using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.User.Admin;

namespace WebShop.Domain.User.Auth
{
    public class UserLoginResponse
    {
        public bool Status { get; set; }
        public string StatusMsg { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}
