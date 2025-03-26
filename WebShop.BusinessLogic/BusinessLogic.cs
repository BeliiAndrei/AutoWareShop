using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.BLogic;
using WebShop.BusinessLogic.Interfaces;

namespace WebShop.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IUser GetUserBl()
        {
            return new UserBL();
        }

        public IProduct GetProductBl()
        {
            return new ProductBL();
        }

        public IPaymnet GetPaymnetBl()
        {
            return new PaymentBL();
        }

        public IBasket GetBasketBL()
        {
            return new BasketBL();
        }

        public IDelivery GetDeliveryBL()
        {
            return new DeliveryBL();
        }

        public IOrder GetOrderBL()
        {
            return new OrderBL();
        }
    }
}
