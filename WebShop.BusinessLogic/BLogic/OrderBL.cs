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

        public List<OrderDTO> GetUserOrders(int userId)
        {
            var ordersDB = GetUserOrdersAction(userId);
            var orders = new List<OrderDTO>();
            foreach (var o in ordersDB)
            {
                var order = new OrderDTO
                {
                    Id = o.Id,
                    Comment = o.Comment,
                    CreationDate = o.CreationDate,
                    EstimatedDeliveryDate = o.EstimatedDeliveryDate,
                    IsPayed = o.IsPayed,
                    IsPickup = o.IsPickup,
                    PaymentMethod = o.PaymentMethod,
                    Price = o.Price,
                    Status = o.Status
                };
                orders.Add(order);
            }
            return orders;
        }
        public List<OrderDTO> GetAllOrders()
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
            order.OrderedProducts = GetOrderProductsByIdAction(id);
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
