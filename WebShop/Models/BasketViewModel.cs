using System.Collections.Generic;

namespace WebShop.Models
{
    public class BasketViewModel
    {
        public int ProductCount { get; set; }
        public List<ProductCardViewModel> ProductsInBasket { get; set; }
        public decimal TotalPrice { get; set; }


    }
}