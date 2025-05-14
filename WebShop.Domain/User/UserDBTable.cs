using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Domain.User.Auth
{
    public class UserDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username cannot be longer than 30 characters.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Usersurname")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Usersurname cannot be longer than 30 characters.")]
        public string Usersurname { get; set; }

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
        public string PhoneNumber { get; set; }

        [Display(Name = "LoginTime")]
        [DataType(DataType.Date)]
        public DateTime LoginTime { get; set; }

        [Display(Name = "Role")]
        public Enumerables.UserRole Level { get; set; }

        [Display(Name = "Balance")]
        public decimal Balance { get; set; }



    }
}
