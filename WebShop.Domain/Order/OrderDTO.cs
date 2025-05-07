using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Enumerables;
using WebShop.Domain.Product;

namespace WebShop.Domain.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentType PaymentMethod { get; set; }
        public bool IsPayed { get; set; }
        public bool IsPickup { get; set; }
        public List<ProductsInOrderDBTable> OrderedProducts { get; set; }
        public string Comment { get; set; }
    }
}
