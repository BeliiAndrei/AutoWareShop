using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class UserInfoModel
    {
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public decimal Balance { get; set; }
    }
}