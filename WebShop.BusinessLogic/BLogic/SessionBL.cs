using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Auth;
using WebShop.Domain.User.Registration;

namespace WebShop.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public HttpCookie GenCookie(string loginCredential)
        {
            throw new NotImplementedException();
        }

        public UserInfo GetUserByCookie(string apiCookieValue)
        {
            throw new NotImplementedException();
        }

        public UserLoginResponse UserLogin(UserLoginData data)
        {
            return UserLoginAction(data);
        }

        public UserRegistrationResponse UserRegistration(UserRegistrationData data)
        {
            return UserRegistrationAction(data);
        }
    }
}
