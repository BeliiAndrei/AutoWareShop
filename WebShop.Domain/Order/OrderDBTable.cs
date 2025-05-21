using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Enumerables;
using WebShop.Domain.Product;

namespace WebShop.Domain.Order
{
    public class OrderDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EstimatedDeliveryDate { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public PaymentType PaymentMethod { get; set; }
        [Required]
        public bool IsPayed { get; set; }
        [Required]
        public bool IsPickup { get; set; }
        public string Comment { get; set; }
    }
}
