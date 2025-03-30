using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Admin;

namespace WebShop.BusinessLogic.BLogic
{
    internal class AdminBL : AdminApi, IAdmin
    {
        public List<UserInfo> GetUsersList()
        {
            var users = GetAllUsers();
            var usersList = new List<UserInfo>();
            foreach (var u in users)
            {
                UserInfo m = new UserInfo();
                m.UserName = u.Username;
                m.UserLastName = u.Usersurname;
                m.Email = u.Email;
                m.PhoneNumber = u.PhoneNumber;
                m.Balance = 0;
                m.Role = u.Level.ToString();
                usersList.Add(m);
            }
            return usersList;
        }
    }
}
