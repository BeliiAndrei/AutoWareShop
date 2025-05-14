using System;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Auth;
using WebShop.Domain.User.Registration;
using WebShop.Domain.User.Admin;
using WebShop.Models.User;
using System.Web.UI.WebControls;
using System.Web;

namespace WebShop.Controllers
{
    public class AuthController : Controller
    {

        private readonly ISession _session;
        private readonly IBasket _basket;
        public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _basket = bl.GetBasketBL();
        }

        public ActionResult Authorisation()
        {

            return View();
        }
        public void StoreUserInSession(UserInfo user)
        {
            SessionHelper.User = user;
            SessionHelper.ProductsInCartCount = _basket.GetBasketSize(user.Id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TryToLogIn(UserLoginModel loginData)// метод авторизации 
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
                    HttpCookie cookie = _session.GenCookie(loginData.Email);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

         
                    StoreUserInSession(userLoginResponse.UserInfo);
                    return RedirectToAction("MainPage", "Home");
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
        public ActionResult RegisterNewUser(UserRegisterModel registerData)// регистрация  
        {
            if (registerData.Password != registerData.RePassword)
            {
                TempData["Message"] = "Пароли не совпадают";
                return View("Registration");
            }
            if (ModelState.IsValid)
            {
                var data = new UserRegistrationData
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
                {
                    var user = userRegister.User;
                    var userForSession = new UserInfo
                    {
                        Id = user.Id,
                        UserName = user.Username,
                        UserLastName = user.Usersurname,
                        Email = user.Email,
                        Balance = user.Balance,
                        PhoneNumber = user.PhoneNumber,
                        Role = user.Level.ToString()
                    };
                    SessionHelper.User = userForSession;
                    return RedirectToAction("MainPage", "Home");
                }
                else
                {
                    TempData["Message"] = userRegister.StatusMsg;
                    return View("Registration");
                }
            }
            TempData["Message"] = "Something went wrong";
            return View("Registration");
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("MainPage", "Home");
        }
    }
}