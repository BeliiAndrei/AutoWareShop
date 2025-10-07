using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.ResBook;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IResBook
    {
        bool CreateReservation(ResBook reservation);
        List<ResBook> GetAllReservations();
        ResBook GetReservationById(int id);
        bool UpdateReservation(ResBook updatedReservation);
        bool DeleteReservation(int id);
        List<ResBook> GetReservationsByEmail(string email);
    }
}
