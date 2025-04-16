using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.Domain.Product;

namespace WebShop.Models.Search
{
    public class AdminProductsModel
    {
        public PageInfo Page { get; set; }
        public int TotalCount { get; set; }
        public List<ProductDTO> Products { get; set; }

    }
}