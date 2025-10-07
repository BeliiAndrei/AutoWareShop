using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Domain.ResBook
{
    public class ResBookDBTab
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название книги")]
        public string Name { get; set; }

        [Display(Name = "Жанр")]
        public string Genre { get; set; }

        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Ваше имя")]
        public string YouName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; } // Изменил на string

        [Display(Name = "Дополнительная информация")]
        public string Info { get; set; }
    }
}