using System.Collections.Generic;
using WebShop.Domain.News;


namespace WebShop.BusinessLogic.Interfaces
{

    public interface INews
    {
        List<News> GetAllNews();
        News GetNewsByIdAction(int id);
        bool UpdateNews(News updatedNews);
        NewsDBTable CreateNews(NewsDBTable newNews);
    }
}
