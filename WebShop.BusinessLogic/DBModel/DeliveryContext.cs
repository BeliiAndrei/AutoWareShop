using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.News;
using WebShop.Domain.User.Delivery;

namespace WebShop.BusinessLogic.DBModel
{
     public class DeliveryContext : DbContext
    {

        public DeliveryContext() : base("name=WebShop")
        {
        }

        public virtual DbSet<DeliveryLocDBTable> Delivery { get; set; }

    }
}
