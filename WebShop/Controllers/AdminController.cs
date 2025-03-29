using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class AdminController : Controller
    {
        public AdminController() 
        {
            var bl = new BusinessLogic.BusinessLogic();
        }
        // GET: Admin
        public ActionResult Admin_pan()
        {
            return View();
        }

        public ActionResult Clients()
        {
            var action = new BusinessLogic.Core.AdminApi();
            var users = action.GetAllUsers();
            var usersList = new List<UserInfoModel>();
            foreach (var u in users)
            {
                UserInfoModel m = new UserInfoModel();
                m.UserName = u.Username;
                m.UserLastName = u.Usersurname;
                m.Email = u.Email;
                m.PhoneNumber = u.PhoneNumber;
                m.Balance = 0;
                m.Role = u.Level.ToString();
                usersList.Add(m);
            }
            return View(usersList);
        }
    }
}