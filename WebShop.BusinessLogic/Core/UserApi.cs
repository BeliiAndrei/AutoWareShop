using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.DBModel;
using WebShop.Domain.Product;
using WebShop.Domain.User.Auth;
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
                    StatusMsg = "UserFound"
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


        public bool IsSessionValidAction(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            return true;
        }

        public bool IsProductValidAction(int id)
        {
            return true;
        }

        internal int GetUserIdBySessionKeyAction(string sessionKey)
        {


            //select from db 

            return 1;

        }



        //---------------------------------PAYMENT-------------------------------

        internal ReceiptToPay GetReceiptToPayByUserIdAction(int uId)
        {


            return new ReceiptToPay();

        }



        //-----------------------------------------------------------------------
    }
}
