using System.Collections.Generic;
using System.Linq;
using WebShop.BusinessLogic.DBModel;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.News;
using WebShop.BusinessLogic.Core;
using System.Data.Entity.Migrations;
using System;
using WebShop.Domain.News.Image;

namespace WebShop.BusinessLogic.BLogic
{
    internal class NewsBL : NewsApi, INews
    {
        public List<News> GetAllNews()
        {
            return GetNewsListAPI().Select(MapToNews).ToList();
        }

        public News GetNewsByIdAction(int id)
        {
            var newsById = GetNewsByIdAPI(id);
            return newsById != null ? MapToNews(newsById) : null;
        }

        public bool UpdateNews(News updatedNews)
        {

            var dbNews = GetNewsByIdAPI(updatedNews.Id);
            if (dbNews == null)
                return false;

            dbNews = MapToDB(updatedNews);

            using (var db = new NewsContext())
            {
                db.News.AddOrUpdate(dbNews);
                db.SaveChanges();

            }
        
            return true;
        }

        public bool CreateNews(News newNews)
        {
            if (string.IsNullOrWhiteSpace(newNews.Title) ||
                string.IsNullOrWhiteSpace(newNews.Author))
            {
                return false;
            }
            var dbNews = MapToDB(newNews);
            using (var context = new NewsContext())
            {
                context.News.Add(dbNews);
                context.SaveChanges();

            }
        return true;
        }

        private NewsDBTable MapToDB(News news)
        {
            return new NewsDBTable
            {
                Id = news.Id,
                Title = news.Title,
                Content = news.Content,
                Author = news.Author,
                Category = news.Category,
                Tags = news.Tags,
                Images = news.Images?.Select(img => new NewsImage
                {
                    ContentType = img.ContentType,
                    ImageData = Convert.FromBase64String(img.Base64Data) // преобразование из base64 в byte[]
                }).ToList()
            };
        }

        // Преобразование из NewsDBTable (Entity) в News (DTO)
        private News MapToNews(NewsDBTable db)
        {
            return new News
            {
                Id = db.Id,
                Title = db.Title,
                Content = db.Content,
                Author = db.Author,
                Category = db.Category,
                Tags = db.Tags,
                Images = db.Images?.Select(img => new NewsImageDto
                {
                    Id = img.Id,
                    ContentType = img.ContentType,
                    Base64Data = Convert.ToBase64String(img.ImageData) // преобразование из byte[] в base64
                }).ToList()
            };
        }
    }
}
