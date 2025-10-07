using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.DBModel;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.ResBook;
using System.Data.Entity;
namespace WebShop.BusinessLogic.Core
{
    public class ResBookApi
    {
        public bool CreateReservationAPI(ResBookDBTab reservation)
        {
            try
            {
                using (var context = new ResBookContext())
                {
                    context.Reservations.Add(reservation);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ResBookDBTab> GetAllReservationsAPI()
        {
            using (var context = new ResBookContext())
            {
                return context.Reservations.ToList();
            }
        }

        public ResBookDBTab GetReservationByIdAPI(int id)
        {
            using (var context = new ResBookContext())
            {
                return context.Reservations.FirstOrDefault(r => r.Id == id);
            }
        }

        public bool UpdateReservationAPI(ResBookDBTab updatedReservation)
        {
            try
            {
                using (var context = new ResBookContext())
                {
                    var existing = context.Reservations.FirstOrDefault(r => r.Id == updatedReservation.Id);
                    if (existing == null) return false;

                    existing.Name = updatedReservation.Name;
                    existing.Genre = updatedReservation.Genre;
                    existing.Author = updatedReservation.Author;
                    existing.YouName = updatedReservation.YouName;
                    existing.Email = updatedReservation.Email;
                    existing.PhoneNumber = updatedReservation.PhoneNumber;
                    existing.Info = updatedReservation.Info;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteReservationAPI(int id)
        {
            try
            {
                using (var context = new ResBookContext())
                {
                    var reservation = context.Reservations.Find(id);
                    if (reservation == null) return false;

                    context.Reservations.Remove(reservation);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ResBookDBTab> GetReservationsByEmailAPI(string email)
        {
            using (var context = new ResBookContext())
            {
                return context.Reservations.Where(r => r.Email == email).ToList();
            }
        }
    }
}
