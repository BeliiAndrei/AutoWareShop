using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class ChangePasswordModel
    {
        public int Id { get; set; }
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } 
        public string ConfirmPassword { get; set;} 
        //public string StatusMessage { get; set; }
    }
}