using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain;
using WebShop.Domain.Delivery.Admin;
using WebShop.Domain.News;

namespace WebShop.BusinessLogic.Interfaces { 

public interface IDelivery
    {
        bool CreateDeliveryInfo(DeliveryLocation newDelivery);
        bool EditDeliveryInfo(DeliveryLocation newDelivery);
        bool DeleteDeliveryInfo(int id);
        DeliveryLocation GetDeliveryLocationByIdAction(int id);
        
    }
}
