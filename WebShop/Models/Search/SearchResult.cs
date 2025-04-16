using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models.Search
{
    public class SearchResult
    {
        public int TotalCount { get; set; }
        public string SearchString { get; set; }
        public List<ProductCardViewModel> Products { get; set; }
    }
}