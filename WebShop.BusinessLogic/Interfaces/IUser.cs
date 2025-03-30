using WebShop.Domain.User.Auth;
using WebShop.Domain.User.Registration;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IUser
    {
        UserLoginResponse UserLoginAction(UserLoginData data);
        UserRegistrationResponse UserRegistrationAction(UserRegistrationData data);
        int GetUserIdBySessionKey(string sessionKey);
        bool IsSessionValid(string key);
        UserRegistrationResponse UserRegistrationAction(UserLoginData data);
    }
}
