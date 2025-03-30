using System.Collections.Generic;
using System.Linq;
using WebShop.BusinessLogic.DBModel;
using WebShop.Domain.User.Auth;

namespace WebShop.BusinessLogic.Core
{
    public class AdminApi
    {
        public AdminApi() { }
        public List<UserDBTable> GetAllUsers()
        {
            using (var db = new UserContext())
            {
                return db.Users.ToList();
            }
        }
        internal bool RegisterUser()
        {
            return true;
        }
        internal bool EditUser()
        {
            return true;
        }

        internal bool EditProduct()
        {
            return true;
        }
        internal bool DeleteUser()
        {
            return true;
        }
        internal bool DeleteProduct()
        {
            return true;
        }
        internal bool AddProduct()
        {
            return true;
        }
    }
}
