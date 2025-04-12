﻿using System;
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

      


        //====================DeliveryLocation=========================
        public class DeliveryLocation
        {

            [Required]
            public string PostalCode { get; set; }
            [Required]
            [StringLength(100)]
            public string City { get; set; }

            [StringLength(200)]
            public string Street { get; set; }

            public string House { get; set; }

            public string Block { get; set; }

            public string Apartment { get; set; }

            [StringLength(500)]

            public string Comment { get; set; }
        }
       

        //============================================================


    }
}
