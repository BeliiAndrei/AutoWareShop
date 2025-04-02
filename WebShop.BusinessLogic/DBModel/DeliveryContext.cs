using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain;
using WebShop.Domain.Delivery;
using WebShop.Domain.User.Auth;

namespace WebShop.BusinessLogic.DBModel
{
    internal class DeliveryContext : DbContext
    {
        public DeliveryContext() :
            base("name=WebShop")
        {

        }

        public virtual DbSet<DeliveryLocationDBTable> DeliveryLocations { get; set; }

    }
}
