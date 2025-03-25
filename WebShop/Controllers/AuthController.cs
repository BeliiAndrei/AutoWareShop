using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Domain.User.Auth;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Authorisation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TryToLogIn(UserLogin loginData)
        {
            if (ModelState.IsValid)
            {
                UserLoginData data = new UserLoginData
                {
                    Email = loginData.Email,
                    Password = loginData.Password,
                    LoginDateTime = DateTime.Now
                };
            }

            if (loginData.Email != null || loginData.Password != null)
            {
                return View("../Home/MainPage");
            }
            else {
                return View("Authorisation");
            }
        }
        public ActionResult Registration()
        {
            return View();
        }
    }
}