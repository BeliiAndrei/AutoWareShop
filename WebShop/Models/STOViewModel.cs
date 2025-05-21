using System;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class STOViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CarBrand { get; set; }
        public string EngineVolume { get; set; }
        public string Year { get; set; }
        public string Vin { get; set; }
        public string Licenseplate { get; set; }

        [Required(ErrorMessage = "Пожалуйста, выберите дату записи")]
        public DateTime RegistrationData { get; set; }

        [Required(ErrorMessage = "Пожалуйста, выберите время записи")]
        public string TimeOfRegistration { get; set; }

        [Required(ErrorMessage = "Пожалуйста, выберите автосервис")]
        public string BranchNumber { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }

    }
}