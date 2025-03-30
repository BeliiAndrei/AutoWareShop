using System;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Auth;
using WebShop.Domain.User.Registration;

namespace WebShop.BusinessLogic.BLogic
{
    public class UserBL : UserApi, IUser
    {
        

        public int GetUserIdBySessionKey(string sessionKey)
        {
            return GetUserIdBySessionKeyAction(sessionKey);
        }

        public bool IsSessionValid(string key)
        {
            return IsSessionValidAction(key);
        }

        public UserRegistrationResponse UserRegistrationAction(UserLoginData data)
        {
            throw new NotImplementedException();
        }

        UserLoginResponse IUser.UserLoginAction(UserLoginData auth)
        {
            return UserLoginAction(auth);
        }

        UserRegistrationResponse IUser.UserRegistrationAction(UserRegistrationData data)
        {
            return UserRegistrationAction(data);
        }
    }
}
