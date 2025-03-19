using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain
{
    public class DeliveryLocation
    {
        public string City { get; set; }
        public string Street { get; set; }
        public uint House { get; set; }
        public uint Block { get; set; }
        public uint Apartment { get; set; }
        public string Comment { get; set; }
        public bool IsSelfDelivery { get; set; }
    }
}
