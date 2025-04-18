using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Order
{
    public class ProductsInOrderDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal PositionPrice { get; set; }
        [Required]
        public int UserId { get; set; }
        
    }
}
