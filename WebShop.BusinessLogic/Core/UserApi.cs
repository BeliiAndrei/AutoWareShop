using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using System.Web;
using WebShop.BusinessLogic.BLogic;
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
using WebShop.Domain.User.Session;
using WebShop.Helpers;
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


        //================================= Balance / Payment ==============================

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


        //----------------------------------Cookie------------------------------------

        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }

        internal UserInfo GetUserIdBySessionKeyAction(string sessionKey)
        {

            return null;

        }

        internal UserInfo UserCookie(string cookie)
        {
            Session session;
            UserDBTable curentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                }
            }

            if (curentUser == null) return null;
            Mapper.Initialize(cfg => cfg.CreateMap<UserDBTable, UserInfo>());
            var userminimal = Mapper.Map<UserInfo>(curentUser);

            return userminimal;
        }



    }
}

