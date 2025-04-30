using System.Data.Entity;
using WebShop.Domain.Order;

namespace WebShop.BusinessLogic.DBModel
{
    internal class OrderContext : DbContext
    {
        public OrderContext() :
            base("name=WebShop")
        {

        }
        public virtual DbSet<OrderDBTable> Orders { get; set; }
    }
}
