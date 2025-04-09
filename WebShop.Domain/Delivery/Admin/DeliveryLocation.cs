﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Delivery.Admin
{
    public class DeliveryLocation
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Block { get; set; }
        public string Apartment { get; set; }
        public string Comment { get; set; }
    }
}
