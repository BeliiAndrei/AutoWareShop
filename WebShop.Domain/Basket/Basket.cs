using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Product;

namespace WebShop.Domain.Basket
{
    public class Basket
    {
        public List<ProductDTO> ProductsInBasket { get; set; }
        public uint ProductCount { get; set; }
    }
}
