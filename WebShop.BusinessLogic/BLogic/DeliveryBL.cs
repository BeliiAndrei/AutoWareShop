using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain;
using WebShop.Domain.Delivery.Admin;


namespace WebShop.BusinessLogic.BLogic
{
    public class DeliveryBL : DeliveryApi, IDelivery
    {
        public void DeleteDeliveryInfo(DeliveryLocation delivery)
        {
            throw new NotImplementedException();
        }

        public DeliveryLocation DeleteDeliveryInfo()
        {
            throw new NotImplementedException();
        }

        public void EditDeliveryInfo(DeliveryLocation delivery)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public void GetDeliveryInfo(int userId)
        {
            throw new NotImplementedException();
        }

        public void GetDeliveryInfo(DeliveryLocation newDelivery)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        DeliveryLocation IDelivery.EditDeliveryInfo(DeliveryLocation newDelivery)
        {
            throw new NotImplementedException();
        }
    }
}
