using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.DBModel;
using WebShop.Domain.User.Delivery;

namespace WebShop.BusinessLogic.Core
{
    public class DeliveryApi
    {
        public void AddDeliveryAddressApi(DeliveryLocDBTable address)
        {

            using (var db = new DeliveryContext())
            {
               db.Delivery.Add(address);    
               db.SaveChanges();
            }
        }
        public DeliveryLocDBTable GetDeliveryAddressByUserIdApi(int id)
        {
            using (var db = new DeliveryContext())
            {
                var address = db.Delivery.FirstOrDefault(x => x.UserId == id);
                return address;
            }
        }
        public void EditDeliveryAddressApi(DeliveryLocDBTable address)
        {
            using (var db = new DeliveryContext())
            {
                var existingAddress = db.Delivery.FirstOrDefault(x => x.Id == address.Id);
                if (existingAddress != null)
                {
                    existingAddress.PostalCode = address.PostalCode;
                    existingAddress.City = address.City;
                    existingAddress.Street = address.Street;
                    existingAddress.House = address.House;
                    existingAddress.Block = address.Block;
                    existingAddress.Apartment = address.Apartment;
                    existingAddress.Comment = address.Comment;
                    db.SaveChanges();
                }
            }
        }
        public void DeleteDeliveryAddressApi(int id)
        {
            using (var db = new DeliveryContext())
            {
                var address = db.Delivery.FirstOrDefault(x => x.Id == id);
                if (address != null)
                {
                    db.Delivery.Remove(address);
                    db.SaveChanges();
                }
            }
        }
        public List<DeliveryLocDBTable> GetAllDeliveryAddressesApi()
        {
            using (var db = new DeliveryContext())
            {
                return db.Delivery.ToList();
            }
        }



    }
}
