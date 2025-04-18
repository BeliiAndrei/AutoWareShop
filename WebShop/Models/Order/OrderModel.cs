using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.Domain.Enumerables;

namespace WebShop.Models.Order
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        //public DateTime Created { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string Comment { get; set; }
        public decimal Price { get; set; }
        public bool isPaid { get; set; }
        public bool isReceived { get; set; }
        public List<ProductCardViewModel> Products { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryType { get; set; }  

    }
}