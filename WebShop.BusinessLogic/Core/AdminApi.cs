using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using WebShop.BusinessLogic.DBModel;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Auth;
using WebShop.Domain.User.Registration;

namespace WebShop.BusinessLogic.Core
{
    public class AdminApi
    {
        public AdminApi() { }
        internal List<UserDBTable> GetAllUsers()
        {
            using (var db = new UserContext())
            {
                return db.Users.ToList();
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
        internal UserRegistrationResponse RegisterUserAction(UserRegistrationData data)
        {
            using (var db = new UserContext())
            {
                var user = new UserDBTable
                {
                    Username = data.UserName,
                    Usersurname = data.UserLastName,
                    Password = data.Password,
                    Email = data.Email,
                    PhoneNumber = data.PhoneNumber,
                    LoginTime = DateTime.Now,
                    Level = data.Role
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
                        StatusMsg = "User added successfully"
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
        public void EditUser(UserDBTable user)
        {
            using (var db = new UserContext())
            {
                db.Users.AddOrUpdate(user);
                db.SaveChanges();
            }
        }

        internal bool EditProduct()
        {
            return true;
        }
        internal bool DeleteUser()
        {
            return true;
        }
        internal bool DeleteProduct()
        {
            return true;
        }
        internal bool AddProduct()
        {
            return true;
        }
    }
}
