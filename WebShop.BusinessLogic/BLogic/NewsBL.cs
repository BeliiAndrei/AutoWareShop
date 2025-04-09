using System.Collections.Generic;
using System.Linq;
using WebShop.BusinessLogic.DBModel;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.News;
using WebShop.BusinessLogic.Core;
using System.Data.Entity.Migrations;
using System;
using WebShop.Domain.User.Auth;
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

            var mappedNews = MapToDB(updatedNews);

            var result = UpdateNewsAPI(mappedNews);

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

            CreateNewsAPI(dbNews);

            return true;
        }

        public bool DeleteNews(int id)
        {
            if (id <= 0) return false;

            try
            {
                DeleteNewsAPI(id);
                return true;
            }
            catch
            {
                return false;
            }
        }


        private NewsDBTab MapToDB(News news)
        {
            return new NewsDBTab
            {
                Id = news.Id,
                Title = news.Title,
                Content = news.Content,
                Author = news.Author,
                Category = news.Category,
                Tags = news.Tags,
                ImageData = news.ImageData,
                ImageMimeType = news.ImageMimeType

            };
        }

        private News MapToNews(NewsDBTab db)
        {
            return new News
            {
                Id = db.Id,
                Title = db.Title,
                Content = db.Content,
                Author = db.Author,
                Category = db.Category,
                Tags = db.Tags,
                ImageData = db.ImageData,
                ImageMimeType = db.ImageMimeType
            };
        }
    }
}
