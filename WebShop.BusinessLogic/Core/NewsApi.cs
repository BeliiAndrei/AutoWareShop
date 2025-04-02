using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.BusinessLogic.DBModel;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.News;

namespace WebShop.BusinessLogic.Core
{
    public class NewsApi 
    {
        public List<NewsDBTable> GetNewsList()
        {
            using (var context = new NewsContext())
                return context.News.ToList();
        }

        public NewsDBTable GetNewsByIdAction(int id)
        {
            using (var context = new NewsContext())
                return context.News.Find(id);
        }

        public NewsDBTable UpdateNews(NewsDBTable updatedNews)
        {
            using (var context = new NewsContext())
            {
                var existingNews = context.News.Find(updatedNews.Id);
                if (existingNews == null) return null;

                context.Entry(existingNews).CurrentValues.SetValues(updatedNews);
                context.SaveChanges();
                return existingNews;
            }
        }

        public void CreateNews(NewsDBTable newNews)
        {
            using (var context = new NewsContext())
            {
                context.News.Add(newNews);
                context.SaveChanges();
            }
        }

        public void DeleteNews(int id)
        {
            using (var context = new NewsContext())
            {
                var news = context.News.Find(id);
                if (news == null) return;

                context.News.Remove(news);
                context.SaveChanges();
            }
        }
    }
}
