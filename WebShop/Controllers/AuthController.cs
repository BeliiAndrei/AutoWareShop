using System;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Auth;
using WebShop.Models;
using WebShop.Domain.User.Registration;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterNewUser(UserRegistrationData registerData)
        {
            if(registerData.Password != registerData.RePassword)
            {
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
                    Console.WriteLine(userRegister.StatusMsg);
                    return View("Registration");
                }
            }
            return View("Registration");
        }
    }
}