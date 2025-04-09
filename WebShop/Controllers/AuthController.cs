using System;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Auth;
using WebShop.Models;
using WebShop.Domain.User.Registration;
using WebShop.Domain.User.Admin;
using WebShop.Domain.Delivery.Admin;
using WebShop.BusinessLogic;
using WebShop.Domain.Delivery;

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
        public ActionResult Delivery(DeliveryViewModel model)
        {
            try
            {
                if (model == null)
                {
                    TempData["ErrorMessage"] = "Неверные данные формы";
                    return RedirectToAction("Delivery");
                }

                if (!ModelState.IsValid)
                {
                    // Возвращаем ту же модель с ошибками валидации
                    return View(model);
                }

                // Конвертируем ViewModel в Domain Model
                var deliveryLocation = ViewTODelivery(model);

                // Сохраняем данные (предполагаем, что CreateDeliveryInfo возвращает bool)
                bool isSuccess = _delivery.CreateDeliveryInfo(deliveryLocation);

                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Адрес доставки успешно сохранён";
                    return View(model);
                }
                else
                {
                    TempData["ErrorMessage"] = "Ошибка при сохранении адреса";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                TempData["ErrorMessage"] = "Произошла ошибка: " + ex.Message;
                return View(model);
            }
        }


        public DeliveryLocation ViewTODelivery (DeliveryViewModel nam)
        {
            var del = new DeliveryLocation
            {
                City = nam.City,
                Street = nam.Street,
                House = nam.House,
                Block = nam.Block,
                Apartment = nam.Apartment,
                PostalCode = nam.PostalCode,
                Comment = nam.Comment


            };
            return del;
        }

        //==============================End_Delivery=====================================
    }
}