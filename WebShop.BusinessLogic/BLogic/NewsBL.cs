using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.BusinessLogic.DBModel;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.News;
using WebShop.BusinessLogic.Core;
using System.Data.Entity.Migrations;



namespace WebShop.BusinessLogic.BLogic
{
    internal class NewsBL: NewsApi, INews
    {
        public List<News> GetAllNews()
        {
            var news = GetNewsList();
            var newsList = new List<News>();
            foreach (var u in news)
            {
                News m = new News();
                m.Id = u.Id;
                m.Title = u.Title;
                m.Content = u.Content;
                m.CreationDate = u.PublishedDate;
                m.Author = u.Author;
                m.Category = u.Category;
                m.Tags = u.Tags;
                m.Images = u.ImageString;
                newsList.Add(m);
            }
            return newsList;
        }

        public News GetNewsByIdAction(int id)
        {
            var newsById = GetNewsById(id);
            var news = new News();
            news.Id = newsById.Id;
            news.Title = newsById.Title;
            news.Content = newsById.Content;
            news.CreationDate = newsById.PublishedDate;
            news.Author = newsById.Author;
            news.Category = newsById.Category;
            news.Tags = newsById.Tags;
            news.Images = newsById.ImageString;
            return news;
        }

        public bool UpdateNews(News updatedNews)
        {
        
            var DBNews = GetNewsById(updatedNews.Id);
            if (DBNews == null)
                return false;
            DBNews.Title = updatedNews.Title;
            DBNews.Content = updatedNews.Content;
            DBNews.PublishedDate = updatedNews.CreationDate;
            DBNews.Author = updatedNews.Author;
            DBNews.Category = updatedNews.Category;
            DBNews.Tags = updatedNews.Tags;
            DBNews.ImageString = updatedNews.Images;

            using (var db = new NewsContext())
            {
                db.News.AddOrUpdate(DBNews);
                db.SaveChanges();
            }

            return true;
        }

        public NewsDBTable CreateNews(NewsDBTable newNews)
        {
            using (var context = new NewsContext())
            {
                context.News.Add(newNews);
                context.SaveChanges();
            }
            return newNews;
        }
    }
}
