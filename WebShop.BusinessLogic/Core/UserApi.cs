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
            UserDBTable user;
            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Email == data.Email);

            }

            // !!! Отключена проверка пароля, вернуть потом обратно !!!  \\
            // +++ Уже вернул обратно. Но если снова надо будет отключить, то закомментить следубщие 7 строк. +++  \\
            var encPassword = LoginRegisterHelper.HashGen(data.Password);
            if (user.Password != encPassword)
                return new UserLoginResponse
                {
                    Status = false,
                    StatusMsg = "Wrong Password"
                };
            if (user != null)
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
            return new UserLoginResponse
            {
                Status = false,
                StatusMsg = "UserNotFound"
            };

        }
        
        internal UserRegistrationResponse UserRegistrationAction(UserRegistrationData data)
        {
            using (var db = new UserContext())
            {
                var isSuchEmail = db.Users.FirstOrDefault(u => u.Email == data.Email);
                if (isSuchEmail != null)
                    return new UserRegistrationResponse
                    {
                        Status = false,
                        StatusMsg = "Such Email already exists"
                    };
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
        public UserDBTable GetUserByIdAction(int id)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.Find(id);
                if (user == null)
                {
                    return null;
                }
                return user;
            }
        }

        internal UserDBTable EditUserProfileAction(UserInfo data)
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

        internal bool ChangePasswordInDBAction(ChangePasswordClass pass)
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

        //=========================== Basket ===========================
        internal List<BasketDTO> GetAllProductsInBasketAction(int userId)
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

        internal BasketActionResponse AddToBasketAction(int userId, int productId, int quantity)
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
                return (new BasketActionResponse
                {
                    Status = true,
                    StatusMsg = "Added successfully"
                });
            }
        }

        internal int GetBasketSizeAction(int userId)
        {
            using (var db = new CartContext())
            {
                int totalQuantity = db.Carts
                                        .Where(c => c.UserId == userId)
                                        .Sum(c => (int?)c.Quantity) ?? 0;
                return totalQuantity;
            }
        }

        internal BasketActionResponse RemoveFromBasketAction(List<int> productIds, int userId)
        {
            using (var db = new CartContext())
            {
                var itemsToRemove = db.Carts
                    .Where(c => c.UserId == userId && productIds.Contains(c.ProductInBasketId))
                    .ToList();

                db.Carts.RemoveRange(itemsToRemove);
                db.SaveChanges();
            }

            return new BasketActionResponse
            {
                Status = true,
                StatusMsg = "Deleted successfully"
            };
        }


        //================================= Balance ==============================

        internal bool SupplyBalanceAction(int userId, decimal moneyToAdd)
        {
            using(var db = new UserContext())
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
            using (var orderContext = new OrderContext())
            using (var productsInOrderContext = new ProductsInOrderContext())
            using (var basketContext = new CartContext())
            {
                // 1. Сохраняем заказ
                order.UserId = userId;
                order.CreationDate = DateTime.Now.Date;
                order.EstimatedDeliveryDate = DateTime.Now.AddDays(7).Date;

                orderContext.Orders.Add(order);
                orderContext.SaveChanges();

                var newOrderId = order.Id;

                // 2. Получаем данные из корзины
                var cartItems = basketContext.Carts
                    .Where(c => c.UserId == userId && productIds.Contains(c.ProductInBasketId))
                    .ToList();

                // 3. Создаём записи для таблицы ProductsInOrderDBTable
                using (var productContext = new ProductContext())
                {
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

                            // Уменьшить количество доступных товаров
                            productFromDb.Quantity -= cartItem.Quantity;
                        }
                        else
                        {
                            return new OrderActionResponse
                            {
                                OrderId = newOrderId,
                                Status = false,
                                StatusMsg = "Товар с id = " + cartItem.Id + " не доступен или недостаточное количество"
                            };
                        }
                    }
                    productContext.SaveChanges(); 
                }

                productsInOrderContext.SaveChanges();

                var response = RemoveFromBasketAction(productIds, userId);
                if(response.Status == true)
                    return new OrderActionResponse
                    {
                        Status = true,
                        StatusMsg = "Заказ успешно создан",
                        OrderId = newOrderId
                    };
                return new OrderActionResponse
                {
                    Status = false,
                    StatusMsg = "Где-то произошла ошибка",
                    OrderId = newOrderId
                };
            }
        }

        internal OrderDBTable GetOrderByIdAction(int id)
        {
            using (var db = new OrderContext())
            {
                var order = db.Orders.Find(id);
                if (order == null)
                {
                    return null;
                }
                return order;
            }
        }

        internal List<OrderDBTable> GetUserOrdersAction(int userId)
        {
            using (var db = new OrderContext())
            {
                var orders = db.Orders.Where(o => o.UserId == userId).ToList();
                return orders;
            }
        }

        internal List<ProductsInOrderDBTable> GetOrderProductsByIdAction(int id)
        {
            using (var db = new ProductsInOrderContext())
            {
                var products = db.ProductsInOrder.Where(p => p.OrderId == id).ToList();
                return products;
            }
        }

        internal bool IsProductValidAction(int id)
        {
            return true;
        }

        internal UserInfo GetUserIdBySessionKeyAction(string sessionKey)
        {

            return null; 

        }



        //---------------------------------PAYMENT-------------------------------

        internal ReceiptToPay GetReceiptToPayByUserIdAction(int uId)
        {


            return new ReceiptToPay();

        }



        //-----------------------------------------------------------------------

       
    }
}
