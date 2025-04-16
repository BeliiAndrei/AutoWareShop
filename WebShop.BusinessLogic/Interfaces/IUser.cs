using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Auth;
using WebShop.Domain.User.Registration;
using WebShop.Domain.User.Modify;
using WebShop.Domain.User.Delivery;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IUser
    {
        UserLoginResponse UserLoginAction(UserLoginData data);
        UserRegistrationResponse UserRegistrationAction(UserRegistrationData data);
        UserInfo EditUserProfile(UserInfo data);
        bool ChangePasswordInDB(ChangePasswordClass pass);
        int GetUserIdBySessionKey(string sessionKey);
        UserInfo GetUserInfoById(int id);

    }
}
