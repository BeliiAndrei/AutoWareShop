using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.STO
{
    public class STO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CarBrand { get; set; }
        public string Model { get; set; }
        public string EngineVolume { get; set; }
        public string Year { get; set; }
        public string Licenseplate{ get; set; }
        public string Vin { get; set; }
        public string BranchNumber { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public DateTime RegistrationData { get; set; }
        public string TimeOfRegistration { get; set; }

    }
}
