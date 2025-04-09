using System.Linq;
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
            var newsList = _news.GetAllNews(); 
            return View(newsList);
        }

    }
}