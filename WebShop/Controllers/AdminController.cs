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
        IAdmin _admin;
        public AdminController() 
        {
            var bl = new BusinessLogic.BusinessLogic();
            _admin = bl.GetAdminBl();
        }
        // GET: Admin
        public ActionResult Admin_pan()
        {
            return View();
        }

        public ActionResult Clients()
        {
            //var action = new BusinessLogic.Core.AdminApi();

            var users = _admin.GetUsersList(); // action.GetAllUsers();
            return View(users);
        }
    }
}