using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Product;

namespace WebShop.Domain.Order
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Warehouse { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public bool IsPayed { get; set; }
        public bool IsSelfDelivery { get; set; }
        public List<ProductDTO> OrderedProducts { get; set; }
        public string Comment { get; set; }
    }
}
