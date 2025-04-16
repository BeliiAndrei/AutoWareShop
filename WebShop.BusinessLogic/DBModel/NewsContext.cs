using System.Data.Entity;
using WebShop.Domain.News;

namespace WebShop.BusinessLogic.DBModel
{
    public class NewsContext : DbContext
    {
        public NewsContext() : base("name=WebShop")
        {
        }

        public virtual DbSet<NewsDBTab> News { get; set; }
    }
}