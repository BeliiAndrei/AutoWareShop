using System.Collections.Generic;
using System.Linq; 
using WebShop.BusinessLogic.DBModel;
using WebShop.Domain.Delivery;
using WebShop.Domain.Delivery.Admin;

namespace WebShop.BusinessLogic.Core
{
    public class DeliveryApi
    {
        public DeliveryApi() { }

        public List<DeliveryLocationDBTable> GetAllDeliveryLocations()
        {
            using (var db = new DeliveryContext())
            {
                return db.DeliveryLocations.ToList();  // ToList() работает с LINQ
            }
        }

        public DeliveryLocationDBTable GetDeliveryLocationById(int id)
        {
            using (var db = new DeliveryContext())
            {
                return db.DeliveryLocations.Where(dl => dl.Id == id).FirstOrDefault();
            }
        }

        internal bool AddDeliveryLocation(DeliveryLocationDBTable location)
        {
            using (var db = new DeliveryContext())
            {
                db.DeliveryLocations.Add(location);
                db.SaveChanges();
                return true;
            }
        }

        internal bool EditDeliveryLocation(DeliveryLocationDBTable location)
        {
            using (var db = new DeliveryContext())
            {
                var existingLocation = db.DeliveryLocations.Where(dl => dl.Id == location.Id).FirstOrDefault();
                if (existingLocation == null)
                {
                    return false;
                }
                db.Entry(existingLocation).CurrentValues.SetValues(location);
                db.SaveChanges();
                return true;
            }
        }

        internal bool DeleteDeliveryLocation(int id)
        {
            using (var db = new DeliveryContext())
            {
                var location = db.DeliveryLocations.Where(dl => dl.Id == id).FirstOrDefault();
                if (location == null)
                {
                    return false;
                }
                db.DeliveryLocations.Remove(location);
                db.SaveChanges();
                return true;
            }
        }
    }
}
