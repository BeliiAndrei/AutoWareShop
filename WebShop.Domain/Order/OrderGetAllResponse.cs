using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Product;

namespace WebShop.Domain.Order
{
    public class OrderGetAllResponse
    {
        public List<OrderDTO> orders { get; set; }
        public int ordersTotalCount { get; set; }
    }
}
