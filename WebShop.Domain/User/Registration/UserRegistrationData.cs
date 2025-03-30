using System;
using WebShop.Domain.Enumerables;

namespace WebShop.Domain.User.Registration
{
    public class UserRegistrationData
    {
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RePassword { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisterTime { get; set; }
        public UserRole Role { get; set; }

    }
}
