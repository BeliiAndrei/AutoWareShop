using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.STO;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface ISTO
    {
        bool CreateSTO(STO newSTO);
        bool UpdateSTO(STO updatedSTO);
        bool DeleteSTO(int id);
        List<STO> GetAllSTO();
        STO GetSTOById(int id);
    }
}
