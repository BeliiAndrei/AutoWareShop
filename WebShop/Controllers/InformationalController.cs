using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.News;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class InformationalController : Controller


   
    {
        INews _news;
        public InformationalController()
    {
        var bl = new BusinessLogic.BusinessLogic();
        _news = bl.GetNewsBl();
    }
        // GET: Informational
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
        [HttpPost]
        public ActionResult GetNewsList()
        {
            var newsList = _news.GetNewsList(); // Получаем список новостей
            return View("NewsList", newsList);  // Передаем в представление NewsList
        }

    }
}