using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.DBModel;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain;
using WebShop.Domain.Delivery;
using WebShop.Domain.Delivery.Admin;
using WebShop.Domain.News;
using WebShop.Domain.User;


namespace WebShop.BusinessLogic.BLogic
{
    public class DeliveryBL : DeliveryApi, IDelivery
    {
        public bool CreateDeliveryInfo(DeliveryLocation newDelivery)
        {
            if (string.IsNullOrWhiteSpace(newDelivery.PostalCode) ||
               string.IsNullOrWhiteSpace(newDelivery.City))
            {
                return false;
            }

            var dbDeliv = MapToDlDB(newDelivery);

            CreateDeliveryInfoAPI(dbDeliv);

            return true;
        }
        public bool EditDeliveryInfo(DeliveryLocation newDelivery)
        {
            var dbDeliv = GetDeliveryLocationByIdActionAPI(newDelivery.Id);
            if (dbDeliv == null)
                return false;

            var mappedNews = MapToDlDB(newDelivery);

            var result = EditDeliveryInfoAPI(mappedNews);

            return true;

        }
        public bool DeleteDeliveryInfo(int id)
        {

            if (id <= 0) return false;

            try
            {
                DeleteDeliveryLocationAPI(id);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public DeliveryLocation GetDeliveryLocationByIdAction(int id)
        {
            using (var db = new DeliveryContext())
            {
                var dbLocation = db.DeliveryLocations.FirstOrDefault(dl => dl.Id == id);
                return dbLocation == null ? null : MapToDl(dbLocation);
            }
        }

        public DeliveryLocation MapToDl(DeliveryLocationDBTable db)
        {
            return new DeliveryLocation
            {
                Id = db.Id,
                PostalCode = db.PostalCode,
                City = db.City,
                Street = db.Street,
                House = db.House,
                Block = db.Block,
                Apartment = db.Apartment,
                Comment = db.Comment
            };

        }
        public DeliveryLocationDBTable MapToDlDB(DeliveryLocation db)
        {
            return new DeliveryLocationDBTable
            {
                Id = db.Id,
                PostalCode = db.PostalCode,
                City = db.City,
                Street = db.Street,
                House = db.House,
                Block = db.Block,
                Apartment = db.Apartment,
                Comment = db.Comment
            };

        }
    }
}
