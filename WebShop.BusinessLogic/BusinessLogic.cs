using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.BLogic;
using WebShop.BusinessLogic.Interfaces;

namespace WebShop.BusinessLogic
{
    class BusinessLogic
    {
        public static ISession GetSessionBL()
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
    }
}
