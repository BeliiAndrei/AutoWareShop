using System;
using System.Linq;
using WebShop.BusinessLogic.DBModel;
using WebShop.Domain.Enumerables;
using WebShop.Domain.Product;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Auth;
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
                if(isSuchEmail != null)
                    return new UserRegistrationResponse
                    {
                        Status = false,
                        StatusMsg = "Such Email already exists"
                    };
                var user = new UserDBTable
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

                // Сохраняем изменения в базе данных
                db.SaveChanges();

                return user;
            };
        }

        public bool ChangePasswordInDBAction(ChangePasswordClass pass)
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
        public bool IsSessionValidAction(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            return true;
        }

        public bool IsProductValidAction(int id)
        {
            return true;
        }

        internal UserInfo GetUserIdBySessionKeyAction(string sessionKey)
        {


            //select from db 

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
