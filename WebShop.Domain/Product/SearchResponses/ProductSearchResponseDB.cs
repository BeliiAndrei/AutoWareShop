﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Product.SearchResponses
{
    public class ProductSearchResponseDB
    {
        public List<ProductDBTable> products { get; set; }
        public int productsTotalCount { get; set; }
        public List<string> availableBrands {  get; set; }
    }
}
