﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Do ()
        {
            throw new NotImplementedException();
        }
    }
}