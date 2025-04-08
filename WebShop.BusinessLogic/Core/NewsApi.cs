using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.BusinessLogic.DBModel;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.News;
using System.Data.Entity;


namespace WebShop.BusinessLogic.Core
{
    public class NewsApi 
    {
        public List<NewsDBTab> GetNewsListAPI()
        {
            using (var context = new NewsContext())
            {
                return context.News.ToList();
            }
        }

        public NewsDBTab GetNewsByIdAPI(int id)
        {
            using (var context = new NewsContext())
            {
                return context.News.FirstOrDefault(n => n.Id == id);
            }
        }

        public NewsDBTab UpdateNewsAPI(News updatedNews)
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

        public void CreateNewsAPI(NewsDBTab newNews)
        {
            using (var context = new NewsContext())
            {
                context.News.Add(newNews);
                context.SaveChanges();
            }
        }

        public void DeleteNewsAPI(int id)
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
