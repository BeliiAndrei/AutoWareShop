using System.ComponentModel.DataAnnotations;

namespace WebShop.Models.User
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Имя должно быть не короче 3 и не длинее 30 символов")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Фамилия должна быть не короче 3 и не длинее 30 символов")]
        public string UserLastName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(4, ErrorMessage = "Минимум 4 символа в пароле")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(4, ErrorMessage = "Минимум 4 символа в пароле")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Укажите Email")]
        [MaxLength(30, ErrorMessage = "Максимум 30 символов в Email-е")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите Номер тлефона")]
        [MaxLength(12, ErrorMessage = "Максимум 12 символов в номере телефона")]
        public string PhoneNumber { get; set; }

        public string Role { get; set; } 

        public decimal Balance { get; set; }
    }
}