using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using WebShop.BusinessLogic.DBModel;
using WebShop.BusinessLogic.DBModel.Seed;
using WebShop.Domain.Basket;
using WebShop.Domain.Enumerables;
using WebShop.Domain.Order;
using WebShop.Domain.Product;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Auth;
using WebShop.Domain.User.Modify;
using WebShop.Domain.User.Registration;
using WebShop.Helpers.LoginRegisterHelper;

namespace WebShop.BusinessLogic.Core
{
    public class UserApi
    {
        public UserApi() { }

        //===================== Login - Register - Edit Profile =======================

        internal UserLoginResponse UserLoginAction(UserLoginData data)
        {
            try
            {
                UserDBTable user;
                using (var db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == data.Email);
                }

                if (user == null)
                {
                    return new UserLoginResponse
                    {
                        Status = false,
                        StatusMsg = "UserNotFound"
                    };
                }

                var encPassword = LoginRegisterHelper.HashGen(data.Password);
                if (user.Password != encPassword)
                {
                    return new UserLoginResponse
                    {
                        Status = false,
                        StatusMsg = "Wrong Password"
                    };
                }

                return new UserLoginResponse
                {
                    Status = true,
                    StatusMsg = "UserFound",
                    UserInfo = new UserInfo
                    {
                        Id = user.Id,
                        UserName = user.Username,
                        UserLastName = user.Usersurname,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Role = user.Level.ToString(),
                        Balance = user.Balance
                    }
                };
            }
            catch (Exception ex)
            {
                return new UserLoginResponse
                {
                    Status = false,
                    StatusMsg = ex.Message
                };
            }
        }

        internal UserRegistrationResponse UserRegistrationAction(UserRegistrationData data)
        {
            try
            {
                using (var db = new UserContext())
                {
                    var isSuchEmail = db.Users.FirstOrDefault(u => u.Email == data.Email);
                    if (isSuchEmail != null)
                    {
                        return new UserRegistrationResponse
                        {
                            Status = false,
                            StatusMsg = "Such Email already exists"
                        };
                    }

                    var encPassword = LoginRegisterHelper.HashGen(data.Password);
                    var user = new UserDBTable()
                    {
                        Username = data.UserName,
                        Usersurname = data.UserLastName,
                        Password = encPassword,
                        Email = data.Email,
                        PhoneNumber = data.PhoneNumber,
                        LoginTime = DateTime.Now,
                        Level = Domain.Enumerables.UserRole.User,
                        Balance = 0m,
                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    // Проверка: Поиск пользователя в БД
                    var savedUser = db.Users.FirstOrDefault(u => u.Id == user.Id);
                    if (savedUser != null)
                    {
                        return new UserRegistrationResponse
                        {
                            Status = true,
                            StatusMsg = "User added successfully",
                            User = savedUser
                        };
                    }
                    else
                    {
                        return new UserRegistrationResponse
                        {
                            Status = false,
                            StatusMsg = "Something went wrong. User was not found after creation attempt"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new UserRegistrationResponse
                {
                    Status = false,
                    StatusMsg = ex.Message,
                    User = null
                };
            }
        }

        public UserDBTable GetUserByIdAction(int id)
        {
            try
            {
                using (var db = new UserContext())
                {
                    return db.Users.Find(id);
                }
            }
            catch
            {
                return null;
            }
        }

        internal UserDBTable EditUserProfileAction(UserInfo data)
        {
            try
            {
                using (var db = new UserContext())
                {
                    var user = db.Users.SingleOrDefault(u => u.Id == data.Id); // или u.Email == model.Email
                    if (user == null)
                    {
                        return null;
                    }

                    // Обновляем поля пользователя
                    user.Username = data.UserName;
                    user.Usersurname = data.UserLastName;
                    user.PhoneNumber = data.PhoneNumber;
                    user.Email = data.Email;
                    user.Balance = data.Balance;

                    db.SaveChanges();

                    return user;
                }
            }
            catch
            {
                return null;
            }
        }

        internal bool ChangePasswordInDBAction(ChangePasswordClass pass)
        {
            try
            {
                using (var db = new UserContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Id == pass.Id);
                    if (user == null)
                        return false;

                    var encPasswordOld = LoginRegisterHelper.HashGen(pass.OldPassword);
                    if (user.Password != encPasswordOld)
                        return false;

                    var encPasswordNew = LoginRegisterHelper.HashGen(pass.NewPassword);
                    user.Password = encPasswordNew;

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        //=========================== Basket ===========================

        internal List<BasketDTO> GetAllProductsInBasketAction(int userId)
        {
            try
            {
                using (var cartDb = new CartContext())
                {
                    var userCarts = cartDb.Carts
                                          .Where(c => c.UserId == userId)
                                          .ToList();

                    var productIds = userCarts.Select(c => c.ProductInBasketId).ToList();

                    using (var productDb = new ProductContext())
                    {
                        var products = productDb.Products
                                                .Where(p => productIds.Contains(p.Id))
                                                .ToList();

                        var result = userCarts.Select(cart =>
                        {
                            var product = products.First(p => p.Id == cart.ProductInBasketId);
                            return new BasketDTO
                            {
                                Product = new ProductDTO
                                {
                                    Id = product.Id,
                                    Name = product.Name,
                                    Price = product.Price,
                                    Producer = product.Producer,
                                    Article = product.Article,
                                    Category = product.Category,
                                    Description = product.Description,
                                    Status = product.Status,
                                    ImageNumber = product.ImageString,
                                    Quantity = product.Quantity
                                },
                                Quantity = cart.Quantity
                            };
                        }).ToList();

                        return result;
                    }
                }
            }
            catch
            {
                return new List<BasketDTO>();
            }
        }

        internal BasketActionResponse AddToBasketAction(int userId, int productId, int quantity)
        {
            try
            {
                using (var db = new CartContext())
                {
                    var existingItem = db.Carts
                        .FirstOrDefault(c => c.UserId == userId && c.ProductInBasketId == productId);

                    if (existingItem != null)
                    {
                        existingItem.Quantity += quantity;
                        db.Carts.AddOrUpdate(existingItem);
                    }
                    else
                    {
                        var newItem = new BasketBDTables
                        {
                            UserId = userId,
                            ProductInBasketId = productId,
                            Quantity = quantity
                        };
                        db.Carts.Add(newItem);
                    }

                    db.SaveChanges();
                    return new BasketActionResponse
                    {
                        Status = true,
                        StatusMsg = "Added successfully"
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

        internal int GetBasketSizeAction(int userId)
        {
            try
            {
                using (var db = new CartContext())
                {
                    int totalQuantity = db.Carts
                                          .Where(c => c.UserId == userId)
                                          .Sum(c => (int?)c.Quantity) ?? 0;
                    return totalQuantity;
                }
            }
            catch
            {
                return 0;
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


        //================================= Balance ==============================

        internal bool SupplyBalanceAction(int userId, decimal moneyToAdd)  //Используется и для списания со счёта, но moneyToAdd приходит со знаком "-"
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                    return false;
                user.Balance += moneyToAdd;
                db.SaveChanges();
                return true;
            }
        }

        //================================= Order ================================

        internal OrderActionResponse CreateNewOrderAction(OrderDBTable order, int userId, List<int> productIds)
        {
            int newOrderId = -1;

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
    }
}
