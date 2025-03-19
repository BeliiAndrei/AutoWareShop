using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;

namespace WebShop.BusinessLogic.BLogic
{
    public class ProductBL : UserApi, IProduct
    {
        public bool IsProductValid(int id)
        {
            return IsProductValidAction(id);
        }
    }
}
