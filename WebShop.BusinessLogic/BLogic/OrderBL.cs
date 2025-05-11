using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.DBModel;
using WebShop.BusinessLogic.DBModel.Seed;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Enumerables;
using WebShop.Domain.Order;
using WebShop.Domain.Product;
using WebShop.Domain.Product.SearchResponses;

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
        public OrderGetAllResponse GetAllOrders(int page, int pageSize)
        {
            //Работа с базой прямо тут, так как эта опция администратора и не имеет отношения к UserApi
            //наверное, следует вынести все действия с Orders в отдельный OrderApi ~~
            try
            {
                using (var db = new OrderContext())
                {
                    var ordersDB = db.Orders.OrderBy(p => p.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    var count = db.Orders.Count();
                    var ordersDTO = new List<OrderDTO>();
                    foreach (var o in ordersDB)
                    {
                        var el = new OrderDTO
                        {
                            Comment = o.Comment,
                            CreationDate = o.CreationDate,
                            EstimatedDeliveryDate = o.EstimatedDeliveryDate,
                            Id = o.Id,
                            IsPayed = o.IsPayed,
                            IsPickup = o.IsPickup,
                            PaymentMethod = o.PaymentMethod,
                            Price = o.Price,
                            Status = o.Status,
                            UserId = o.UserId
                        };
                        ordersDTO.Add(el);
                    }
                    var response = new OrderGetAllResponse
                    {
                        orders = ordersDTO,
                        ordersTotalCount = count
                    };
                    return response;
                }
            }
            catch (Exception)
            {
                return new OrderGetAllResponse
                {
                    orders = null,
                    ordersTotalCount = 0
                };
            }
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
            throw new NotImplementedException();
        }

        public OrderActionResponse ModifyOrderStatus(int id, OrderStatus newStatus)
        {
            //Работа с базой прямо тут, так как статус может изменять только администратор и он не имеет отношения к UserApi
            using (var db = new OrderContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == id);
                if (order != null)
                {
                    order.Status = newStatus;
                    db.SaveChanges();
                    return new OrderActionResponse
                    {
                        OrderId = id,
                        Status = true,
                        StatusMsg = "Status modified successfully"
                    };
                }
                return new OrderActionResponse
                {
                    OrderId = id,
                    Status = false,
                    StatusMsg = "Order was not found"
                };
            }
        }

        public List<OrderDTO> SortOrdersByDate(List<OrderDTO> orders)
        {
            throw new NotImplementedException();
        }

        public OrderDTO UpdateOrderPrice(int orderId, decimal newPrice)
        { 
            //Этот метод на данный используется в случае, если оформление заказа неудачное и нужно пометить новый заказ как отменённый и приравнять цену нулю.
            using (var db = new OrderContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                if (order != null)
                {
                    order.Price = newPrice;
                    db.SaveChanges();
                }
                var orderDTO = new OrderDTO
                {
                    Status = order.Status,
                    Id = orderId,
                    Comment = order.Comment,
                    CreationDate = order.CreationDate,
                    EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                    IsPayed = order.IsPayed,
                    IsPickup = order.IsPickup,
                    PaymentMethod = order.PaymentMethod,
                    Price = order.Price,
                    UserId = order.UserId
                };
                return orderDTO;
            }
        }
    }
}
