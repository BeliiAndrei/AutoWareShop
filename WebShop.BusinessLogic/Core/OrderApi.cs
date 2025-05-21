using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.DBModel.Seed;
using WebShop.BusinessLogic.DBModel;
using WebShop.Domain.Enumerables;
using WebShop.Domain.Order;
using WebShop.Domain.Basket;
using System.Web.UI;

namespace WebShop.BusinessLogic.Core
{
    public class OrderApi
    {
        //================================= Order ================================

        internal OrderActionResponse CreateNewOrderAction(OrderDBTable order, int userId, List<int> productIds)
        {
            int newOrderId;

            try
            {
                using (var orderContext = new OrderContext())
                {
                    // Сохраняем заказ
                    order.UserId = userId;
                    order.CreationDate = DateTime.Now.Date;
                    order.EstimatedDeliveryDate = DateTime.Now.AddDays(7).Date;

                    orderContext.Orders.Add(order);
                    orderContext.SaveChanges();

                    newOrderId = order.Id;
                }
            }
            catch (Exception ex)
            {
                return new OrderActionResponse
                {
                    Status = false,
                    StatusMsg = "Ошибка при создании заказа: " + ex.Message
                };
            }

            try
            {
                using (var basketContext = new CartContext())
                using (var productsInOrderContext = new ProductsInOrderContext())
                using (var productContext = new ProductContext())
                {
                    // Получаем данные из корзины
                    var cartItems = basketContext.Carts
                        .Where(c => c.UserId == userId && productIds.Contains(c.ProductInBasketId))
                        .ToList();

                    foreach (var cartItem in cartItems)
                    {
                        var productFromDb = productContext.Products.FirstOrDefault(p => p.Id == cartItem.ProductInBasketId);

                        if (productFromDb != null && productFromDb.Status != ProductStatus.hidden && productFromDb.Quantity >= cartItem.Quantity)
                        {
                            var productInOrder = new ProductsInOrderDBTable
                            {
                                ProductId = cartItem.ProductInBasketId,
                                UserId = userId,
                                OrderId = newOrderId,
                                Quantity = cartItem.Quantity,
                                PositionPrice = cartItem.Quantity * productFromDb.Price
                            };

                            productsInOrderContext.ProductsInOrder.Add(productInOrder);
                            productFromDb.Quantity -= cartItem.Quantity;
                        }
                        else
                        {
                            var product = productContext.Products.FirstOrDefault(p => p.Id == cartItem.ProductInBasketId);
                            return new OrderActionResponse
                            {
                                OrderId = newOrderId,
                                Status = false,
                                StatusMsg = $"Товар {product.Name} с атрикулом  {product.Article} недоступен или его недостаточное количество."
                            };
                        }
                    }

                    productContext.SaveChanges();
                    productsInOrderContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return new OrderActionResponse
                {
                    OrderId = newOrderId,
                    Status = false,
                    StatusMsg = "Ошибка при добавлении товаров в заказ: " + ex.Message
                };
            }

            try
            {
                var response = RemoveFromBasketAction(productIds, userId);
                if (response.Status)
                {
                    return new OrderActionResponse
                    {
                        Status = true,
                        StatusMsg = "Заказ успешно создан",
                        OrderId = newOrderId
                    };
                }
                else
                {
                    return new OrderActionResponse
                    {
                        Status = false,
                        StatusMsg = "Заказ создан, но не удалось удалить товары из корзины: " + response.StatusMsg,
                        OrderId = newOrderId
                    };
                }
            }
            catch (Exception ex)
            {
                return new OrderActionResponse
                {
                    Status = false,
                    StatusMsg = "Ошибка при удалении товаров из корзины: " + ex.Message,
                    OrderId = newOrderId
                };
            }
        }

        internal BasketActionResponse RemoveFromBasketAction(List<int> productIds, int userId)
        {
            try
            {
                using (var db = new CartContext())
                {
                    var itemsToRemove = db.Carts
                        .Where(c => c.UserId == userId && productIds.Contains(c.ProductInBasketId))
                        .ToList();

                    db.Carts.RemoveRange(itemsToRemove);
                    db.SaveChanges();

                    return new BasketActionResponse
                    {
                        Status = true,
                        StatusMsg = "Deleted successfully"
                    };
                }
            }
            catch (Exception ex)
            {
                return new BasketActionResponse
                {
                    Status = false,
                    StatusMsg = ex.Message
                };
            }
        }
        internal OrderDBTable GetOrderByIdAction(int id)
        {
            try
            {
                using (var db = new OrderContext())
                {
                    return db.Orders.Find(id);
                }
            }
            catch
            {
                return null;
            }
        }

        internal List<OrderDBTable> GetUserOrdersAction(int userId)
        {
            try
            {
                using (var db = new OrderContext())
                {
                    return db.Orders.Where(o => o.UserId == userId).ToList();
                }
            }
            catch
            {
                return new List<OrderDBTable>();
            }
        }

        internal List<ProductsInOrderDBTable> GetOrderProductsByIdAction(int id)
        {
            try
            {
                using (var db = new ProductsInOrderContext())
                {
                    return db.ProductsInOrder.Where(p => p.OrderId == id).ToList();
                }
            }
            catch
            {
                return new List<ProductsInOrderDBTable>();
            }
        }

        internal OrderGetAllResponse GetAllOrdersAction(int page, int pageSize)
        {
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

        internal OrderActionResponse ModifyOrderStatusAction(int id, OrderStatus newStatus)
        {
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

        internal OrderDBTable UpdateOrderPriceAction(int orderId, decimal newPrice)
        {
            //Этот метод на данный момент используется в случае, если оформление заказа неудачное и нужно пометить новый заказ как отменённый и приравнять цену нулю.
            using (var db = new OrderContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                if (order != null)
                {
                    order.Price = newPrice;
                    db.SaveChanges();
                }
                return order;
            }
        }
    }
}
