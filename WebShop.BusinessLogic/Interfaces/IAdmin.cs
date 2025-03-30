using System.Collections.Generic;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Registration;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IAdmin
    {
        List<UserInfo> GetUsersList();
        UserInfo GetUserById(int id);
        UserInfo UpdateUser(UserInfo oldUser);
        UserRegistrationResponse RegisterUser(UserRegistrationData newUser);

    }
}
