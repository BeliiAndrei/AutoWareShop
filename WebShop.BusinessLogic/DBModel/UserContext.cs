using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.User.Auth;
using WebShop.Domain.User.Delivery;

namespace WebShop.BusinessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext() :
            base("name=WebShop")
        {

        }

        public virtual DbSet<UserDBTable> Users { get; set; }

    }
}
