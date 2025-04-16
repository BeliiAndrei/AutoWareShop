using System;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Auth;
using WebShop.Models;
using WebShop.Domain.User.Registration;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Delivery;


namespace WebShop.Controllers
{
    public class AuthController : Controller
    {

        private readonly ISession _session;
        private readonly IDelivery _delivery;

        public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _delivery = bl.GetDeliveryBL();
        }


        public ActionResult Authorisation()
        {
            return View();
        }
        public void StoreUserInSession(UserInfo user)
        {
            Session["User"] = user;
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

        public ActionResult LogOut()
        {
            Session["User"] = null;
            return RedirectToAction("MainPage", "Home");
        }


        //==============================Delivery=========================================


        public ActionResult Delivery()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delivery(DeliveryViewModel deliveryData)
        {
            if (ModelState.IsValid)
            {
                
                var user = (UserInfo)Session["User"];
                if (user != null)
                {
                    deliveryData.UserId = user.Id;
                    DeliveryL delivery = ViewToL(deliveryData);

                    var deliveryResponse = _delivery.AddDeliveryAddress(delivery);
                }
                TempData["Message"] = "Succes";
                return RedirectToAction("ProfileUser", "Profile");

            }
            TempData["Message"] = "Something went wrerong";
            return View("Delivery");
        }

        public DeliveryL ViewToL(DeliveryViewModel view)
        {
            return new DeliveryL
            {
                UserId = view.UserId,
                PostalCode = view.PostalCode,
                City = view.City,
                Street = view.Street,
                House = view.House,
                Block = view.Block,
                Apartment = view.Apartment,
                Comment = view.Comment
            };
        }


        //==============================End_Delivery=====================================
    }
}