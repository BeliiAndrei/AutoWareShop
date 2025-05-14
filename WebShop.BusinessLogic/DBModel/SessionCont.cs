using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.User.Session;

namespace WebShop.BusinessLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=WebShop")
        {
        }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}
