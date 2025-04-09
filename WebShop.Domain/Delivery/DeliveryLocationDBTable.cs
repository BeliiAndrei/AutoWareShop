using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Domain.Delivery
{
    public class DeliveryLocationDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string PostalCode { get; set; } 

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [StringLength(200)]
        public string Street { get; set; }

        public string House { get; set; }

        public string Block { get; set; } // Не всегда есть блок

        public string Apartment { get; set; } // Не всегда есть квартира

        [StringLength(500)]

        public string Comment { get; set; }
    }
}
