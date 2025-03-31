using System;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Auth;
using WebShop.Models;
using WebShop.Domain.User.Registration;
using WebShop.Domain.User.Admin;
namespace WebShop.Controllers
{
    public class AuthController : Controller
    {
        //System.Web.HttpContext.Current.Session["LoginStatus"] = "login";

        //public System.Web.SessionState.HttpSessionState Session_user { get; }
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
        public void StoreUserInSession(UserInfo user)
        {
            Session["User"] = user;
            //var test = Session["User"];
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TryToLogIn(UserLoginModel loginData)
        {
            if (ModelState.IsValid)
            {
                UserLoginData data = new UserLoginData
                {
                    Email = loginData.Email,
                    Password = loginData.Password,
                    LoginDateTime = DateTime.Now,
                    LoginIp = Request.UserHostAddress
                };
                var userLoginResponse = _session.UserLogin(data);
                if (userLoginResponse.Status == true)
                {
                    StoreUserInSession(userLoginResponse.UserInfo);
                    return View("../Home/MainPage");
                }
                else
                {
                    TempData["Message"] = userLoginResponse.StatusMsg;
                    return View("Authorisation");
                }
            }
            TempData["Message"] = "Something went wrong";
            return View("Authorisation");

        }
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterNewUser(UserRegistrationData registerData)
        {
            if (registerData.Password != registerData.RePassword)
            {
                TempData["Message"] = "Пароли не совпаают";
                return View("Registration");
            }
            if (ModelState.IsValid)
            {
                UserRegistrationData data = new UserRegistrationData
                {
                    UserName = registerData.UserName,
                    UserLastName = registerData.UserLastName,
                    Password = registerData.Password,
                    RePassword = registerData.RePassword,
                    Email = registerData.Email,
                    PhoneNumber = registerData.PhoneNumber,
                    RegisterTime = DateTime.Now,
                };
                var userRegister = _session.UserRegistration(data);
                if (userRegister.Status == true)
                    return View("../Home/MainPage");
                else
                {
                    TempData["Message"] = userRegister.StatusMsg;
                    return View("Registration");
                }
            }
            TempData["Message"] = "Something went wrong";
            return View("Registration");
        }
    }
}