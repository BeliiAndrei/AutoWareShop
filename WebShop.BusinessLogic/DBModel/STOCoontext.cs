using System.Data.Entity;
using WebShop.Domain.STO;


namespace WebShop.BusinessLogic.DBModel
{
    public class STOContext : DbContext
    {
        public STOContext() : base("name=WebShop")
        {
        }

        public virtual DbSet<STODBTab> STO { get; set; }
    }
}
