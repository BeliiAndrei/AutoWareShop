using System;

namespace WebShop.Domain.User.Admin
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public decimal Balance { get; set; }
        public string LasIp { get; set; }
        public DateTime LastLogin { get; set; }


    }
}