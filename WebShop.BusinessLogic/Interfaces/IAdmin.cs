using System.Collections.Generic;
using WebShop.Domain.User.Admin;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IAdmin
    {
        List<UserInfo> GetUsersList();
    }
}
