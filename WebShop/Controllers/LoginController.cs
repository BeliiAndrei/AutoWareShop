//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using AutoMapper;
//using System.Web.Mvc;
//using WebShop.BusinessLogic.Interfaces;
//using WebShop.BusinessLogic;



//namespace WebShop.Controllers
//{
//    public class LoginController : Controller
//    {
//        private readonly ISession _session;
//        public LoginController()
//        {
//            var bl = new BusinessLogic();
//            _session = bl.GetSessionBL();
//        }

//        // GET: Login
//        public ActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Index(UserLogin login)
//        {
//            if (ModelState.IsValid)
//            {
//                Mapper.Initialize(cfg => cfg.CreateMap<UserLogin, ULoginData>());
//                var data = Mapper.Map<ULoginData>(login);

//                data.LoginIp = Request.UserHostAddress;
//                data.LoginDateTime = DateTime.Now;

//                var userLogin = _session.UserLogin(data);
//                if (userLogin.Status)
//                {
//                    HttpCookie cookie = _session.GenCookie(login.Credential);
//                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

//                    return RedirectToAction("Index", "Home");
//                }
//                else
//                {
//                    ModelState.AddModelError("", userLogin.StatusMsg);
//                    return View();
//                }
//            }

//            return View();
//        }
//    }
//}