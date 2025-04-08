using System.Data.Entity;
using WebShop.Domain.News;

namespace WebShop.BusinessLogic.DBModel
{

    internal class NewsContext : DbContext
    {
        public NewsContext() :
            base("name=WebShop")
        {
        }

        public virtual DbSet<NewsDBTable> News { get; set; }
    }
}