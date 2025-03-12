using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Interfaces;

namespace WebShop.BusinessLogic
{
    class BusinessLogic
    {
        public static ISession GetSessionBL()
        {
            return new SessionBL();
        }
    }
}
