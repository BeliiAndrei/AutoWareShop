using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.DBModel.Seed;
using WebShop.Domain.Product;

namespace WebShop.BusinessLogic.Core
{
    public class ProductApi
    {
        public ProductApi() { }
        internal bool CreateNewProductAction(ProductDBTable product)
        {
            using (var db = new ProductContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
            return true;
        }
    }
}
