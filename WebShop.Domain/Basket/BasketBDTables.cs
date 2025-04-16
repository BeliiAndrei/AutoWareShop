using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Product;

namespace WebShop.Domain.Basket
{
    public class BasketBDTables
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "UserId")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "ProductInBasket")]
        public int ProductInBasketId { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }
}
