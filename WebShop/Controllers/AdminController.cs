using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Admin_pan()
        {
            return View();
        }

        public ActionResult Clients()
        {
            return View();
        }
    }
}