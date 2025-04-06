using System.Data.Entity;
using WebShop.Domain.Basket;

namespace WebShop.BusinessLogic.DBModel
{
    internal class CartContext : DbContext
    {
        public CartContext() :
            base("name=WebShop")
        {

        }

        public virtual DbSet<BasketBDTables> Carts { get; set; }
    }
}
