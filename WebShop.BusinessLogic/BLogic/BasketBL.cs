using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Basket;
using WebShop.Domain.Product;

namespace WebShop.BusinessLogic.BLogic
{
    public class BasketBL : UserApi, IBasket
    {
        public BasketActionResponse AddToBasket(int userId, int productId, int quantity)
        {
            return AddToBasketAction(userId, productId, quantity);
        }

        public List<BasketDTO> GetAllProductsInCart(int userId)
        {
            return GetAllProductsInBasketAction(userId);
        }

        public int GetBasketSize(int userId)
        {
            return GetBasketSizeAction(userId);
        }

        public BasketActionResponse RemoveFromBasket(List<string> productIdsAsString, int userId)
        {
            List<int> productIds = new List<int>();
            foreach (string productId in productIdsAsString)
            {
                if (int.TryParse(productId, out int id))
                {
                    productIds.Add(id);
                }
            }
            return RemoveFromBasketAction(productIds, userId);
        }
    }
}
