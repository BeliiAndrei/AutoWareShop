using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.Domain.Product;
using WebShop.Models.Order;
using WebShop.Models.Search;

namespace WebShop.Models.Order
{
    public class AdminOrdersModel
    {
        public PageInfo Page { get; set; }
        public int TotalCount { get; set; }
        public List<OrderModel> Orders { get; set; }
    }
}