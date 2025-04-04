﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.BusinessLogic.DBModel;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.News;
using WebShop.BusinessLogic.Core;


namespace WebShop.BusinessLogic.BLogic
{
    internal class NewsBL: NewsApi, INews
    {
        public List<NewsDBTable> GetNewsList()
        {
            using (var context = new NewsContext())
            {
                return context.News.ToList();
            }
        }

        public NewsDBTable GetNewsByIdAction(int id)
        {
            using (var context = new NewsContext())
            {
                return context.News.FirstOrDefault(news => news.Id == id);
            }
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
    }
}
