using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain;
using WebShop.Domain.Delivery.Admin;

namespace WebShop.BusinessLogic.Interfaces { 

public interface IDelivery
    {

        DeliveryLocation EditDeliveryInfo(DeliveryLocation newDelivery);
        DeliveryLocation DeleteDeliveryInfo();
        void GetDeliveryInfo(DeliveryLocation newDelivery);
    }
}
