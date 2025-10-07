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

        public NewsDBTab UpdateNewsAPI(NewsDBTab updatedNews)
        {
            using (var context = new NewsContext())
            {
                var existingNews = context.News.FirstOrDefault(n => n.Id == updatedNews.Id);
                if (existingNews == null)
                    return null;

                existingNews.Title = updatedNews.Title;
                existingNews.Content = updatedNews.Content;
                existingNews.Author = updatedNews.Author;
                existingNews.Category = updatedNews.Category;
                existingNews.Tags = updatedNews.Tags;
                existingNews.ImageData = updatedNews.ImageData;
                existingNews.ImageMimeType = updatedNews.ImageMimeType;

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
                if (news == null) throw new ArgumentException("News not found");

                context.News.Remove(news);
                context.SaveChanges();
            }
        }
    }
}
