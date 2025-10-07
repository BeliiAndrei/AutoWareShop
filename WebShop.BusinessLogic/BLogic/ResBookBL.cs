using System.Collections.Generic;
using System.Linq;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.ResBook;

namespace WebShop.BusinessLogic.BLogic
{
    public class ResBookBL : IResBook
    {
        private readonly ResBookApi _api = new ResBookApi();

        public bool CreateReservation(ResBook reservation)
        {
            var dbModel = new ResBookDBTab
            {
                Name = reservation.Name,
                Genre = reservation.Genre,
                Author = reservation.Author,
                YouName = reservation.YouName,
                Email = reservation.Email,
                PhoneNumber = reservation.PhoneNumber,
                Info = reservation.Info
            };

            return _api.CreateReservationAPI(dbModel);
        }

        public List<ResBook> GetAllReservations()
        {
            var dbReservations = _api.GetAllReservationsAPI();
            return dbReservations.Select(ToDomainModel).ToList();
        }

        public ResBook GetReservationById(int id)
        {
            var dbReservation = _api.GetReservationByIdAPI(id);
            return dbReservation != null ? ToDomainModel(dbReservation) : null;
        }

        public bool UpdateReservation(ResBook updatedReservation)
        {
            var dbModel = new ResBookDBTab
            {
                Id = updatedReservation.Id,
                Name = updatedReservation.Name,
                Genre = updatedReservation.Genre,
                Author = updatedReservation.Author,
                YouName = updatedReservation.YouName,
                Email = updatedReservation.Email,
                PhoneNumber = updatedReservation.PhoneNumber,
                Info = updatedReservation.Info
            };

            return _api.UpdateReservationAPI(dbModel);
        }

        public bool DeleteReservation(int id)
        {
            return _api.DeleteReservationAPI(id);
        }

        public List<ResBook> GetReservationsByEmail(string email)
        {
            var dbReservations = _api.GetReservationsByEmailAPI(email);
            return dbReservations.Select(ToDomainModel).ToList();
        }
        public IResBook GetResBookBl()
        {
            return new ResBookBL();
        }

        private ResBook ToDomainModel(ResBookDBTab dbModel)
        {
            return new ResBook
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Genre = dbModel.Genre,
                Author = dbModel.Author,
                YouName = dbModel.YouName,
                Email = dbModel.Email,
                PhoneNumber = dbModel.PhoneNumber,
                Info = dbModel.Info
            };
        }
    }
}