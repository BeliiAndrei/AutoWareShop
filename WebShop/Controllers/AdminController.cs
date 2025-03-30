using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Registration;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class AdminController : Controller
    {
        IAdmin _admin;
        public AdminController() 
        {
            var bl = new BusinessLogic.BusinessLogic();
            _admin = bl.GetAdminBl();
        }
        [HttpGet]
        public ActionResult ClientProfile()
        {
            return RedirectToActionPermanent("Clients");
        }
        [HttpPost]
        public ActionResult ClientProfile(int userId)
        {
            var user = _admin.GetUserById(userId);
            if (user == null)
            {
                return View("../Error/Error_500");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult UserProfileEdit(UserInfo model)
        {
            if (!ModelState.IsValid)
            {
                return View("ClientProfile", model);
            }
            var user = _admin.UpdateUser(model);
            TempData["SuccessMessage"] = "Изменения успешно сохранены.";
            return View("ClientProfile", user);
        }

        public ActionResult Clients()
        {
            var users = _admin.GetUsersList(); 
            return View(users);
        }

        public ActionResult AddUser()
        {
            var model = new UserRegisterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterUser(UserRegistrationData registerData)
        {
            if (ModelState.IsValid)
            {
                var userRegister = _admin.RegisterUser(registerData);
                TempData["Message"] = userRegister.StatusMsg;
                if (userRegister.Status == true)
                {
                    return RedirectToAction("Clients");
                }
                else
                {
                    return RedirectToAction("AddUser");
                }
            }
            return RedirectToAction("AddUser");
        }
    }
}