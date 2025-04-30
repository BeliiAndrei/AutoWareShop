using System.Data.Entity;
using WebShop.Domain.Order;

namespace WebShop.BusinessLogic.DBModel
{
    internal class ProductsInOrderContext : DbContext
    {
        public ProductsInOrderContext() :
            base("name=WebShop")
        {

        }
        public virtual DbSet<ProductsInOrderDBTable> ProductsInOrder { get; set; }
    }

}
