using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.DBModel;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Enumerables;
using WebShop.Domain.Order;

namespace WebShop.BusinessLogic.BLogic
{
    public class OrderBL : OrderApi, IOrder
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

        public OrderActionResponse DeleteOrder(int id)
        {
            return DeleteOrderAction(id);
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
        public OrderGetAllResponse GetAllOrders(int page, int pageSize)
        {
            return GetAllOrdersAction(page, pageSize);
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
                Status = orderBL.Status,
                UserId = orderBL.UserId
            };
            order.OrderedProducts = GetOrderProductsByIdAction(id);
            return order;
        }

        public decimal GetOrderPrice(int id)
        {
            return GetOrderPriceAction(id);
        }

        public OrderActionResponse ModifyOrderStatus(int id, OrderStatus newStatus)
        {
            return ModifyOrderStatusAction(id, newStatus);
        }

        //public List<OrderDTO> SortOrdersByDate(List<OrderDTO> orders)
        //{
        //    throw new NotImplementedException();
        //}

        public OrderDTO UpdateOrderPrice(int orderId, decimal newPrice)
        {
            OrderDBTable updatedOrder = UpdateOrderPriceAction(orderId, newPrice);
            var orderDTO = new OrderDTO
            {
                Status = updatedOrder.Status,
                Id = orderId,
                Comment = updatedOrder.Comment,
                CreationDate = updatedOrder.CreationDate,
                EstimatedDeliveryDate = updatedOrder.EstimatedDeliveryDate,
                IsPayed = updatedOrder.IsPayed,
                IsPickup = updatedOrder.IsPickup,
                PaymentMethod = updatedOrder.PaymentMethod,
                Price = updatedOrder.Price,
                UserId = updatedOrder.UserId
            };
            return orderDTO;
        }
    }
}
