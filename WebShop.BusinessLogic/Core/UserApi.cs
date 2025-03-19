using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Product;
using WebShop.Domain.User.Auth;

namespace WebShop.BusinessLogic.Core
{
    public class UserApi
    {


        public UserApi() { }

        public bool IsSessionValidAction(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            return true;
        }

        public bool IsProductValidAction(int id)
        {
            return true;
        }

        public string AuthentificateUserAction(UserPhyAuthAction auth)
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
