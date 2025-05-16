using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.News;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class InformationalController : BaseController



    {
        INews _news;
        public InformationalController()
    {
        var bl = new BusinessLogic.BusinessLogic();
        _news = bl.GetNewsBl();
    }
        
        public ActionResult About_1()
        {
            return View();
        }

        public ActionResult About_2()
        {
            return View();
        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Vacation()
        {
            return View();
        }

        public ActionResult ViewNews()
        {


            List<NewsViewModel> newsList = LNewsToLView(); // Преобразуем список News в NewsViewModel
            return View(newsList); // Передаем правильный тип в представление


        }
        private NewsViewModel NewsToView(News db)
        {
            return new NewsViewModel
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


        private List<NewsViewModel> LNewsToLView()
        {
            var newsList = _news.GetAllNews();

            if (newsList == null)
            {
                return new List<NewsViewModel>();
            }
            var News2 = new List<NewsViewModel>();
            foreach (var n in newsList)
            {
                var news = new NewsViewModel();
                news = NewsToView(n);
                News2.Add(news);
            }
            //var Ne = NewsToView()
            //return newsList.Select(NewsToView).ToList();
            return News2;
        }
    }
}