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
    public class BasketBL : UserApi, IBasket
    {
        public void AddToBasket(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public void DecrementProductCount(int productId, uint currentCount)
        {
            throw new NotImplementedException();
        }

        public List<ProductDTO> GetAllProductsInCart(int userId)
        {
            throw new NotImplementedException();
        }

        public void IncrementProductCount(int productId, uint currentCount)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromBasket(int productId, uint count)
        {
            throw new NotImplementedException();
        }
    }
}
