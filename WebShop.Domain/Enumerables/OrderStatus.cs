using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Enumerables
{
    public enum OrderStatus
    {
        Cancelled = 0,
        Delivered = 1,
        Received = 2,
        Pending = 3,
    }
}
