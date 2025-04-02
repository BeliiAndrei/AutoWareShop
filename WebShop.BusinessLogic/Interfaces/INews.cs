using System.Collections.Generic;
using WebShop.Domain.News;

public interface INews
{
    List<NewsDBTable> GetNewsList();
    NewsDBTable GetNewsByIdAction(int id);
    NewsDBTable UpdateNews(NewsDBTable updatedNews);
    void CreateNews(NewsDBTable newNews);
}
