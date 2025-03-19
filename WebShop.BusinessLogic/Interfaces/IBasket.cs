using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Product;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IBasket
    {
        List<ProductDTO> GetAllProductsInCart(int userId);
        void RemoveFromBasket(int productId, uint count);
        void AddToBasket(ProductDTO product);
        void IncrementProductCount(int productId, uint currentCount);
        void DecrementProductCount(int productId, uint currentCount);
        void RemoveAll();

    }
}
