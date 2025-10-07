using System.Data.Entity;
using WebShop.Domain.ResBook;

namespace WebShop.BusinessLogic.DBModel
{
    public class ResBookContext : DbContext
    {
        public ResBookContext() : base("name=WebShop")
        {
        }

        public virtual DbSet<ResBookDBTab> Reservations { get; set; }
    }
}