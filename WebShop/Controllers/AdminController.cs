using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic.BLogic;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.News;
using WebShop.Domain.Product;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Registration;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class AdminController : Controller
    {
        IAdmin _admin;
        IProduct _product;
        INews _news;

        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _admin = bl.GetAdminBl();
            _product = bl.GetProductBl();
            _news = bl.GetNewsBl();
        }

        [HttpGet]
        public ActionResult ManageNews()
        {
            var model = new NewsDBTable();
            return View(model);
        }

        [HttpPost]
        public ActionResult GetNewsList()
        {
            var newsList = _news.GetAllNews();
            return View("NewsList", newsList);
        }

        [HttpPost]
        public ActionResult GetNewsByIdAction(int id)
        {
            var news = _news.GetNewsByIdAction(id);
            if (news == null)
            {
                return View("../Error/Error_500");
            }
            return View("EditNews", news);
        }


        [HttpPost]
        public ActionResult AddNews(FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Введённые данные некорректны";
                return View("ManageNews");
            }

            var newNews = new NewsDBTable
            {
                Title = form["Title"],
                Content = form["Content"],
                Author = form["Author"],
                Category = form["Category"],
                Tags = form["Tags"],
                PublishedDate = DateTime.Now,
                ImageString = form["ImageString"]
            };

            try
            {
                _news.CreateNews(newNews);
                TempData["Message"] = "Новость успешно создана.";
                return RedirectToAction("ManageNews");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var error in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {error.PropertyName}, Error: {error.ErrorMessage}");
                    }
                }
                TempData["Message"] = "Ошибка создания новости. Проверьте введенные данные.";
                return View("ManageNews", newNews);
            }
        }

        //[HttpPost]
        //public ActionResult DeleteNews(int id)
        //{
        //    _news.DeleteNews(id);
        //    TempData["Message"] = "Новость успешно удалена.";
        //    return RedirectToAction("ManageNews");
        //}

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

        public ActionResult Products()
        {
            var products = _product.GetProductsList();
            return View(products);
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
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Введённые данные некорректны";
                return View();
            }
            var response = _product.CreateNewProduct(model);
            if (response.Status == true)
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