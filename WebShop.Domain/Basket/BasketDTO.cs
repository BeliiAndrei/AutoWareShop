using System.Collections.Generic;
using WebShop.Domain.Product;

namespace WebShop.Domain.Basket
{
    public class BasketDTO
    {
        //public int BasketID { get; set; }
        //public int UserId { get; set; }
        //public List<ProductDTO> ProductsInBasket { get; set; }
        //public int ProductCount { get; set; }

        //public int ProductId { get; set; }
        //public string Name { get; set; }      // или другие нужные свойства товара
        //public decimal Price { get; set; }
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }     
    }
}
