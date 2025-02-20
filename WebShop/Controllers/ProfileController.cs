using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult Profile_user()
        {
            return View();
        }
    }
}