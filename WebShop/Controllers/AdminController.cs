using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic.BLogic;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Product;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Registration;
using WebShop.Models;
using WebShop.Models.Search;

namespace WebShop.Controllers
{
    public class AdminController : Controller
    {
        IAdmin _admin;
        IProduct _product;
        public AdminController() 
        {
            var bl = new BusinessLogic.BusinessLogic();
            _admin = bl.GetAdminBl();
            _product = bl.GetProductBl();
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
                    TempData["Message"] = userRegister.StatusMsg;
                    return RedirectToAction("AddUser");
                }
            }
            TempData["Message"] = "Something went wrong";
            return RedirectToAction("AddUser");
        }

        public ActionResult Products(int page = 1)
        {
            int pageSize = 10;
            var response = _product.GetProductsList(page, pageSize);
            var model = new AdminProductsModel
            {
                Page = new PageInfo(response.productsTotalCount, page, pageSize),
                Products = response.products,
                TotalCount = response.productsTotalCount
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductDTO model)
        {
            if(!ModelState.IsValid)
            {
                TempData["Message"] = "Введённые данные некорректны";
                return View();
            }
            var response = _product.CreateNewProduct(model);
            if(response.Status == true)
            {
                TempData["Message"] = response.StatusMsg;
                return RedirectToAction("Products");
            }
            else
            {
                TempData["Message"] = response.StatusMsg;
                return View();
            }
        }

        [HttpPost]
        public ActionResult ProductProfile(int productId)
        {
            var product = _product.GetProductById(productId);
            if (product == null)
            {
                return View("../Error/Error_500");
            }
            return View(product);
        }
        [HttpGet]
        public ActionResult ProductProfile()
        {
            return RedirectToActionPermanent("Products");
        }

        [HttpPost]
        public ActionResult ProductProfileEdit(ProductDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductProfile", model);
            }
            var product = _product.ModifyProduct(model);
            TempData["Message"] = "Изменения успешно сохранены.";
            return View("ProductProfile", product);
        }

    }
}