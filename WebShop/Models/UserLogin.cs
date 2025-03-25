using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Укажите Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Range(0, 100, ErrorMessage = "Минимум 4 символа в пароле")]
        public string Password { get; set; }
    }
}