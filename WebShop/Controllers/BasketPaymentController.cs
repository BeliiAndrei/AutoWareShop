using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.Controllers
{
    public class BasketPaymentController : Controller
    {
        public ActionResult Basket_step_1()
        {
            int productCount = 12; //передавать количество в дальнейшем из бэка. Сейчас число тестовое
            if(productCount == 0)
                return RedirectToAction("Empty_basket");
            return View();
        }
        public ActionResult Basket_step_2()
        {
            return View();
        }
        public ActionResult Basket_step_3()
        {
            return View();
        }

        public ActionResult Basket_step_4()
        {
            return View();
        }

        public ActionResult Empty_basket()
        {
            return View();
        }
        public ActionResult Payment()
        {
            return View();
        }
    }
}