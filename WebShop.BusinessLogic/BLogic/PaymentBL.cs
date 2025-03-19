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
    public class PaymentBL : UserApi, IPaymnet
    {
        public ReceiptToPay GetReceiptToPayByUserId(int uId)
        {
            return GetReceiptToPayByUserIdAction(uId);
        }
    }
}
