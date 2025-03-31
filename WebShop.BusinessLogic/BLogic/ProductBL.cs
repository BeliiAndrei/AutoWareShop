using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Product;

namespace WebShop.BusinessLogic.BLogic
{
    public class ProductBL : ProductApi, IProduct
    {
        public bool CreateNewProduct(ProductDBTable data)
        {
            return CreateNewProductAction(data);
        }

        public bool IsProductValid(int id)
        {
            return IsProductValidAction(id);
        }

        private bool IsProductValidAction(int id)
        {
            throw new NotImplementedException();
        }
    }
}
