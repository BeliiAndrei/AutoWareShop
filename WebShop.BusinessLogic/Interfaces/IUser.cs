using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.User.Auth;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IUser
    {
        string AuthentificateUser(UserAuthAction auth);
        int GetUserIdBySessionKey(string sessionKey);
        bool IsSessionValid(string key);
    }
}
