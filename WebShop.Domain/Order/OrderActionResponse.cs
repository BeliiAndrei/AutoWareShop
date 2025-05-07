using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Order
{
    public class OrderActionResponse
    {
        public bool Status { get; set; }
        public string StatusMsg { get; set; }
        public int OrderId { get; set; }
    }
}
