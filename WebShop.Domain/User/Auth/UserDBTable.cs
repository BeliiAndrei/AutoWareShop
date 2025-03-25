using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WebShop.Domain.User.Auth
{
    public class UserDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Username cannot be longer than 30 characters.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Password cannot be shorter than 4 characters.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "PhoneNumber")]
        [StringLength(12)]

        public string MobilePhoneNumber { get; set; }

        public string HomePhoneNumber { get; set; }

        public string City { get; set; }

        [DataType(DataType.Date)]
        public DateTime LoginTime { get; set; }

        public Enumerables.UserRole Level { get; set; }
    }
}
