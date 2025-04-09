using System.Collections.Generic;
using System.Linq; 
using WebShop.BusinessLogic.DBModel;
using WebShop.Domain.Delivery;
using WebShop.Domain.Delivery.Admin;
using System;

namespace WebShop.BusinessLogic.Core
{
    public class DeliveryApi
    {


        public bool CreateDeliveryInfoAPI(DeliveryLocationDBTable location)
        {
            using (var db = new DeliveryContext())
            {
                db.DeliveryLocations.Add(location);
                db.SaveChanges();
                return true;
            }
        }
        public bool EditDeliveryInfoAPI(DeliveryLocationDBTable location)
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
        public bool DeleteDeliveryLocationAPI(int id)
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

        public DeliveryLocationDBTable GetDeliveryLocationByIdActionAPI(int id)
        {
            using (var db = new DeliveryContext())
            {
                return db.DeliveryLocations.Where(dl => dl.Id == id).FirstOrDefault();
            }
        }

       

        
        
    }
}
