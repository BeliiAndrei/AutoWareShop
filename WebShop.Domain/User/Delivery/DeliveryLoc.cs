using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.User.Admin;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Domain.User.Delivery
{
    public class DeliveryLoc
    {
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Block { get; set; }
        public string Apartment { get; set; }
        public string Comment { get; set; }
    }
}
