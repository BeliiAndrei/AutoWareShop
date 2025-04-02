using System.Data.Entity;
using WebShop.Domain.News;

internal class NewsContext : DbContext
{
    public NewsContext() :
        base("name=WebShop")
    {
    }

    public virtual DbSet<NewsDBTable> News { get; set; }
}
