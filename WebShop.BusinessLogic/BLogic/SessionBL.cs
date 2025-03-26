using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Auth;

namespace WebShop.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public UserLoginResponse UserLogin(UserLoginData data)
        {
            return UserLoginAction(data);
        }
    }
}
