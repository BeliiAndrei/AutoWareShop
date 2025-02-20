using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.Controllers
{
    public class CatalogSearchController : Controller
    {
        // GET: Catalog
        public ActionResult Catalog()
        {
            return View();
        }

        public ActionResult Catalog_brands()
        {
            return View();
        }

        public ActionResult Match_vincode()
        {
            return View();
        }

        public ActionResult Search_result()
        {
            return View();
        }

        public ActionResult Select_car()
        {
            return View();
        }
    }
}