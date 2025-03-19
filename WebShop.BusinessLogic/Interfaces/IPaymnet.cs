using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Product;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IPaymnet
    {
        ReceiptToPay GetReceiptToPayByUserId(int uId);
    }
}
