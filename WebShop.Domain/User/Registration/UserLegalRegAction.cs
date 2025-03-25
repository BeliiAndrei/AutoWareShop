using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.User.Auth
{
    public class UserLegalRegAction
    {
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RePassword { get; set; }
        public string PocketPhoneNumber { get; set; }
        public string StationPhoneNumber { get; set; }
        public string City { get; set; }
        public string CompanyName { get; set; }
        public ulong BINorINN { get; set; }
        public ulong CurrentAccount { get; set; }
        public ulong BankCode { get; set; }
        public ulong LegalAddress { get; set; }
        public DateTime LoginTime { get; set; }


    }
}
