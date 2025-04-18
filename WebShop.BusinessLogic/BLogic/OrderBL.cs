using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Order;
using WebShop.Domain.Product;

namespace WebShop.BusinessLogic.BLogic
{
    public class OrderBL : UserApi, IOrder
    {
        public OrderActionResponse CreateNewOrder(OrderDTO order, int userId, List<int> products)
        {
            OrderDBTable orderDB = new OrderDBTable()
            {
                Status = Domain.Enumerables.OrderStatus.Pending,
                Comment = order.Comment,
                CreationDate = order.CreationDate,
                EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                IsPayed = order.IsPayed,
                IsPickup = order.IsPickup,
                PaymentMethod = order.PaymentMethod,
                Price = order.Price,
                UserId = userId
            };
            return CreateNewOrderAction(orderDB, userId,  products);
        }

        public void DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderDTO> GetAllOrders(int userId)
        {
            throw new NotImplementedException();
        }

        public OrderDTO GetOrderById(int id)
        {
            OrderDBTable orderBL = GetOrderByIdAction(id);
            OrderDTO order = new OrderDTO
            {
                Comment = orderBL.Comment,
                CreationDate = orderBL.CreationDate,
                EstimatedDeliveryDate = orderBL.EstimatedDeliveryDate,
                Id = id,
                IsPayed = orderBL.IsPayed,
                IsPickup = orderBL.IsPickup,
                PaymentMethod = orderBL.PaymentMethod,
                Price = orderBL.Price,
                Status = orderBL.Status
            };
            return order;
        }

        public decimal GetOrderPrice(int id)
        {
            throw new NotImplementedException();
        }

        public void ModifyOrderStatus(int id)
        {
            throw new NotImplementedException();
        }

        public void SortOrdersByDate(List<OrderDTO> orders)
        {
            throw new NotImplementedException();
        }
    }
}
