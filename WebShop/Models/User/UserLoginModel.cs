using System.ComponentModel.DataAnnotations;

namespace WebShop.Models.User
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Укажите Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(4, ErrorMessage = "Минимум 4 символа в пароле")]
        public string Password { get; set; }
    }
}