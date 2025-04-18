﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using WebShop.BusinessLogic.BLogic;
using WebShop.BusinessLogic.DBModel;
using WebShop.BusinessLogic.DBModel.Seed;
using WebShop.Domain.Basket;
using WebShop.Domain.Enumerables;
using WebShop.Domain.Product;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Auth;
using WebShop.Domain.User.Delivery;
using WebShop.Domain.User.Modify;
using WebShop.Domain.User.Registration;

namespace WebShop.BusinessLogic.Core
{
    public class UserApi
    {

        public UserApi() { }

        


        internal UserLoginResponse UserLoginAction(UserLoginData data)
        {
            UserDBTable user;
            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Email == data.Email);

            }
            if (user.Password != data.Password)
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
                        Role = user.Level.ToString()
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
                var user = new UserDBTable()
                {
                    Username = data.UserName,
                    Usersurname = data.UserLastName,
                    Password = data.Password,
                    Email = data.Email,
                    PhoneNumber = data.PhoneNumber,
                    LoginTime = DateTime.Now,
                    Level = Domain.Enumerables.UserRole.User
                };

                db.Users.Add(user);
                db.SaveChanges(); // Сохранение изменений в БД

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

                db.SaveChanges();

                return user;
            }
            ;
        }

        internal bool ChangePasswordInDBAction(ChangePasswordClass pass)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == pass.Id);
                if (user == null)
                    return false;
                if (user.Password != pass.OldPassword)
                    return false;
                user.Password = pass.NewPassword;
                db.SaveChanges();
                return true;
            }
        }


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
