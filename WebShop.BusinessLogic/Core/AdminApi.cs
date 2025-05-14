using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using WebShop.BusinessLogic.DBModel;
using WebShop.Domain.User.Auth;
using WebShop.Domain.User.Registration;
using WebShop.Helpers.LoginRegisterHelper;

namespace WebShop.BusinessLogic.Core
{
    public class AdminApi
    {
        public AdminApi() { }
        internal List<UserDBTable> GetAllUsers()
        {
            try
            {
                using (var db = new UserContext())
                {
                    return db.Users.ToList();
                }
            }
            catch
            {
                return new List<UserDBTable>();
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

        internal UserRegistrationResponse RegisterUserAction(UserRegistrationData data)
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

                    string encPassword;
                    try
                    {
                        encPassword = LoginRegisterHelper.HashGen(data.Password);
                    }
                    catch (Exception ex)
                    {
                        return new UserRegistrationResponse
                        {
                            Status = false,
                            StatusMsg = "Error hashing password: " + ex.Message
                        };
                    }

                    var user = new UserDBTable
                    {
                        Username = data.UserName,
                        Usersurname = data.UserLastName,
                        Password = encPassword,
                        Email = data.Email,
                        PhoneNumber = data.PhoneNumber,
                        LoginTime = DateTime.Now,
                        Level = data.Role,
                        Balance = data.Balance
                    };

                    db.Users.Add(user);
                    db.SaveChanges();

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
            catch (Exception ex)
            {
                return new UserRegistrationResponse
                {
                    Status = false,
                    StatusMsg = "Exception occurred: " + ex.Message
                };
            }
        }

        internal void EditUser(UserDBTable user)
        {
            try
            {
                using (var db = new UserContext())
                {
                    db.Users.AddOrUpdate(user);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update user: " + ex.Message);
            }
        }
    }
}
