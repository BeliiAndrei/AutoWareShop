using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebShop.Domain.User.Auth;
using WebShop.Domain.User.Registration;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface ISession
    {
        UserLoginResponse UserLogin(UserLoginData data);
        UserRegistrationResponse UserRegistration(UserRegistrationData data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);
    }

}
