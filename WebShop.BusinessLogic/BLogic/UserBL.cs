using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Auth;

namespace WebShop.BusinessLogic.BLogic
{
    public class UserBL : UserApi, IUser
    {
        public string AuthentificateUser(UserAuthAction auth)
        {
            return AuthentificateUserAction(auth);
        }

        public int GetUserIdBySessionKey(string sessionKey)
        {
            return GetUserIdBySessionKeyAction(sessionKey);
        }

        public bool IsSessionValid(string key)
        {
            return IsSessionValidAction(key);
        }
    }
}
