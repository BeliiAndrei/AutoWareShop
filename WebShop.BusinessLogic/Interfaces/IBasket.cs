using System.Collections.Generic;
using WebShop.Domain.Basket;
using WebShop.Domain.Product;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IBasket
    {
        List<BasketDTO> GetAllProductsInCart(int userId);
        int GetBasketSize(int userId);
        BasketActionResponse RemoveFromBasket(List <string> productIds, int userId);
        BasketActionResponse AddToBasket(int userId, int productId, int quantity);
        BasketActionResponse RemoveAll(int userId);
    }
}
