using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Authorisation()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }
    }
}