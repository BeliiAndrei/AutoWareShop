using System.Data.Entity;
using WebShop.Domain.Product;

namespace WebShop.BusinessLogic.DBModel.Seed
{
    internal class ProductContext : DbContext
    {
        public ProductContext() :
            base("name=WebShop")
        {

        }

        public virtual DbSet<ProductDBTable> Products { get; set; }
    }
}
