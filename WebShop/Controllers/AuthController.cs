using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Auth;
using WebShop.Models;
using WebShop.BusinessLogic;
namespace WebShop.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISession _session;
        public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }

        // GET: Auth
        public ActionResult Authorisation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TryToLogIn(UserLogin loginData)
        {
            //if (ModelState.IsValid)
            if(true)
            {
                UserLoginData data = new UserLoginData
                {
                    Email = loginData.Email,
                    Password = loginData.Password,
                    LoginDateTime = DateTime.Now,
                    LoginIp = Request.UserHostAddress
                };
            var userLogin = _session.UserLogin(data);
                if(userLogin.Status == true)
                    return View("../Home/MainPage");
                else
                {
                    Console.WriteLine(userLogin.StatusMsg);
                    return View("Authorisation");
                }
            }
            return View("Authorisation");

        }
        public ActionResult Registration()
        {
            return View();
        }
    }
}