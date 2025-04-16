using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Delivery;

namespace WebShop.BusinessLogic.BLogic
{
    internal class DeliveryBL:DeliveryApi, IDelivery
    {
        public bool AddDeliveryAddress(DeliveryL address)
        {
            if (string.IsNullOrWhiteSpace(address.PostalCode) ||
           string.IsNullOrWhiteSpace(address.City))
            {
                return false;
            }
            var db = MapToDB(address);
            AddDeliveryAddressApi(db);
            return true;
        }
        public DeliveryL GetDeliveryAddressByUserId(int Uid)
        {
            var db = GetDeliveryAddressByUserIdApi(Uid);
            if (db == null)
            {
                return null;
            }
            var address = MapToL(db);
            return address;
        }
        public bool EditDeliveryAddress(DeliveryL address, int Uid)
        {

            if (string.IsNullOrWhiteSpace(address.PostalCode) ||
               string.IsNullOrWhiteSpace(address.City))
            {
                return false;
            }
            address.UserId = Uid;
            var db = MapToDB(address);
            EditDeliveryAddressApi(db);
            return true;
        }
        public bool DeleteDeliveryAddress(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            DeleteDeliveryAddressApi(id);
            return true;
        }
        public DeliveryLocDBTable MapToDB(DeliveryL adress)
        {

            return new DeliveryLocDBTable
            {
                Id = adress.Id,
                UserId = adress.UserId,
                PostalCode = adress.PostalCode,
                City = adress.City,
                Street = adress.Street,
                House = adress.House,
                Block = adress.Block,
                Apartment = adress.Apartment,
                Comment = adress.Comment
            };

        }
        public DeliveryL MapToL(DeliveryLocDBTable address)
        {
            return new DeliveryL
            {
                Id = address.Id,
                UserId = address.UserId,
                PostalCode = address.PostalCode,
                City = address.City,
                Street = address.Street,
                House = address.House,
                Block = address.Block,
                Apartment = address.Apartment,
                Comment = address.Comment
            };
        }
    }
    
}
