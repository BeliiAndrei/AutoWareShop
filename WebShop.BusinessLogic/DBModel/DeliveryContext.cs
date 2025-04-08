using System.Data.Entity;
using WebShop.Domain.Delivery;

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
