using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Enumerables;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Registration;

namespace WebShop.BusinessLogic.BLogic
{
    internal class AdminBL : AdminApi, IAdmin
    {
        public UserInfo GetUserById(int id)
        {
            var userFromDB = GetUserByIdAction(id);
            var user = new UserInfo();
            user.Id = userFromDB.Id;
            user.UserName = userFromDB.Username;
            user.UserLastName = userFromDB.Usersurname;
            user.Email = userFromDB.Email;
            user.PhoneNumber = userFromDB.PhoneNumber;
            user.Balance = 0;
            user.Role = userFromDB.Level.ToString();
            return user;
        }

        public List<UserInfo> GetUsersList()
        {
            var users = GetAllUsers();
            var usersList = new List<UserInfo>();
            foreach (var u in users)
            {
                UserInfo m = new UserInfo();
                m.Id = u.Id;
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


        public UserInfo UpdateUser(UserInfo oldUser)
        {
            var user = GetUserByIdAction(oldUser.Id);
            if (user == null)
                return null;
            user.Username = oldUser.UserName;
            user.Usersurname = oldUser.UserLastName;
            user.Email = oldUser.Email;
            user.PhoneNumber = oldUser.PhoneNumber;
            user.Level = (UserRole)Enum.Parse(typeof(UserRole), oldUser.Role);
            EditUser(user);
            return GetUserById(oldUser.Id);
        }

        public UserRegistrationResponse RegisterUser(UserRegistrationData newUser)
        {
            return RegisterUserAction(newUser);
        }
    }

}
