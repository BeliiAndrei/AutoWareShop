using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.User.Delivery;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IDelivery
    {
        bool AddDeliveryAddress(DeliveryL address);
        DeliveryL GetDeliveryAddressByUserId(int id);
        bool EditDeliveryAddress(DeliveryL address, int userId);
        bool DeleteDeliveryAddress(int id);
    }
}
