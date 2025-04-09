using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
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
        ///////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public ActionResult News()
        {

            var model = new NewsViewModel();
            return View(model);

        }

        [HttpPost]
        public ActionResult News(News RegNews)
        {
            var imageFile = Request.Files["ImageFile"];

            using (var binaryReader = new BinaryReader(imageFile.InputStream))
            {
                RegNews.ImageData = binaryReader.ReadBytes(imageFile.ContentLength);
            }
            RegNews.ImageMimeType = imageFile.ContentType;

            if (ModelState.IsValid)
            {
                bool v = _news.CreateNews(RegNews);

                TempData["Message"] = "Succes";
                return RedirectToAction("News");
            }
            TempData["Message"] = "Something went wrong";
            return RedirectToAction("News");
        }
        public ActionResult NewsEditor()
        {
            var newsList = _news.GetAllNews();
            return View(newsList);
        }
        [HttpPost]
        public ActionResult NewsEditor(News RegNews)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNews(int id)
        {
            try
            {
                bool isDeleted = _news.DeleteNews(id);
                if (isDeleted)
                {
                    TempData["Message"] = "Новость успешно удалена";
                    TempData["AlertType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Новость не найдена";
                    TempData["AlertType"] = "warning";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Ошибка при удалении: {ex.Message}";
                TempData["AlertType"] = "danger";
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return RedirectToAction("NewsEditor");
        }
        /////////////////////////////////////////////////////////////////////////////////////////


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
        [HttpGet]
        public ActionResult AddUser()
        {
            var model = new UserRegisterModel();
            return View(model);
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