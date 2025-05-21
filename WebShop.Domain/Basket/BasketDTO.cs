using System.Collections.Generic;
using WebShop.Domain.Product;

namespace WebShop.Domain.Basket
{
    public class BasketDTO
    {
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }     
    }
}
