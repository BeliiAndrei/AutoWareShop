using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebShop.Domain.STO
{
    public class STODBTab
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string CarBrand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string EngineVolume { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string BranchNumber { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string Licenseplate { get; set; }
        public string Vin { get; set; }
        [Required]
        public DateTime RegistrationData { get; set; }
        [Required]
        public string TimeOfRegistration { get; set; }
    }
}
