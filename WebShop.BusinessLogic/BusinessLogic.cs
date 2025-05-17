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
        public IDelivery GetDeliveryBL()
        {
            return new DeliveryBL();
        }
        public IUser GetUserBl()
        {
            return new UserBL();
        }
        public INews GetNewsBl()
        {
            return new NewsBL();
        }

        public IProduct GetProductBl()
        {
            return new ProductBL();
        }
        public ISTO GetSTOBl()
        {
            return new STOBL();
        }

        public IBasket GetBasketBL()
        {
            return new BasketBL();
        }

        public IOrder GetOrderBL()
        {
            return new OrderBL();
        }

        public IAdmin GetAdminBl()
        {
            return new AdminBL();
        }
    }
}
