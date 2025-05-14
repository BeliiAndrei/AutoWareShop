using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Auth;
using WebShop.Domain.User.Registration;
using WebShop.Domain.User.Modify;
using WebShop.Domain.User.Delivery;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IUser
    {
        UserInfo EditUserProfile(UserInfo data);
        bool ChangePasswordInDB(ChangePasswordClass pass);

        UserInfo GetUserInfoById(int id);

        bool SupplyBalance(int userId, decimal moneyToAdd);

    }
}
