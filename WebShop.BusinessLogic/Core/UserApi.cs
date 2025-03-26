using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.DBModel;
using WebShop.Domain.Product;
using WebShop.Domain.User.Auth;

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
            if (user != null)
                return new UserLoginResponse
                {
                    Status = true,
                    StatusMsg = "UserFound"
                }; //tmp solution to end work for today
            return new UserLoginResponse
            {
                Status = false,
                StatusMsg = "UserNotFound"
            };

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

        public string AuthentificateUserAction(UserPhyRegAction auth)
        {





            return "";
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
