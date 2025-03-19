using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IDelivery
    {
        void SetDeliveryInfo();
        void EditDeliveryInfo(DeliveryLocation delivery);
        void DeleteDeliveryInfo(DeliveryLocation delivery);
        void GetDeliveryInfo(int userId);
    }
}
