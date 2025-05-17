using System;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class STOViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }



        [Required(ErrorMessage = "Пожалуйста, выберите марку автомобиля")]
        public string CarBrand { get; set; }

        [Required(ErrorMessage = "Пожалуйста, укажите модель автомобиля")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Пожалуйста, выберите объем двигателя")]
        public string EngineVolume { get; set; }

        [Required(ErrorMessage = "Пожалуйста, выберите год выпуска")]
        public string Year { get; set; }

        public string Vin { get; set; }
        public string Licenseplate { get; set; }

        [Display(Name = "Ваше Имя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Пожалуйста, укажите ваш телефон")]
        [Phone(ErrorMessage = "Некорректный формат телефона")]
        [Display(Name = "Телефон")]
        public string UserPhone { get; set; }

        [Required(ErrorMessage = "Пожалуйста, выберите дату записи")]
        [Display(Name = "Дата записи")]
        public DateTime RegistrationData { get; set; }

        [Required(ErrorMessage = "Пожалуйста, выберите время записи")]
        [Display(Name = "Время записи")]
        public string TimeOfRegistration { get; set; }

        [Required(ErrorMessage = "Пожалуйста, выберите СТО")]
        [Display(Name = "Филиал СТО")]
        public string BranchNumber { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        public string Status { get; set; }
    }
}