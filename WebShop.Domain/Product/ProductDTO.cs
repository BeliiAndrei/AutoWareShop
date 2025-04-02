using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Enumerables;

namespace WebShop.Domain.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public string Article { get; set; }
        public int Quantity { get; set; }
        public string ImageNumber { get; set; }

    }
}
