using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.User.Modify
{
    public class ChangePasswordClass
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Password cannot be shorter than 4 characters.")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Password cannot be shorter than 4 characters.")]
        public string NewPassword { get; set; }
    }
}
