using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.News;
using WebShop.Domain.Product;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Registration;
using WebShop.Models;
using WebShop.Models.Search;

namespace WebShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdmin _admin;
        private readonly IProduct _product;
        private readonly INews _news;

        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _admin = bl.GetAdminBl();
            _product = bl.GetProductBl();
            _news = bl.GetNewsBl();
        }

        // ========== NEWS ==========

        [HttpGet]
        public ActionResult News()
        {
            return View(new NewsViewModel());
        }

        [HttpPost]
        public ActionResult News(NewsViewModel model)
        {
            var news = ViewToNews(model);
            var imageFile = Request.Files["ImageFile"];

            using (var binaryReader = new BinaryReader(imageFile.InputStream))
            {
                news.ImageData = binaryReader.ReadBytes(imageFile.ContentLength);
            }
            news.ImageMimeType = imageFile.ContentType;

            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Something went wrong";
                return RedirectToAction("News");
            }

            _news.CreateNews(news);
            TempData["Message"] = "Success";
            return RedirectToAction("News");
        }

        public ActionResult NewsEditor()
        {
            try
            {
                var newsList = _news.GetAllNews()?.Select(NewsToView).ToList() ?? new List<NewsViewModel>();
                return View(newsList);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при загрузке новостей: {ex}");
                TempData["Message"] = "Произошла ошибка при загрузке новостей";
                TempData["AlertType"] = "danger";
                return View(new List<NewsViewModel>());
            }
        }

 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNews(int id)
        {
            try
            {
                var isDeleted = _news.DeleteNews(id);

                TempData["Message"] = isDeleted
                    ? "Новость успешно удалена"
                    : "Новость не найдена";

                TempData["AlertType"] = isDeleted ? "success" : "warning";
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Ошибка при удалении: {ex.Message}";
                TempData["AlertType"] = "danger";
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return RedirectToAction("NewsEditor");
        }

        public ActionResult ViewNews()
        {
           
            
                List<NewsViewModel> newsList = LNewsToLView(); // Преобразуем список News в NewsViewModel
                return View(newsList); // Передаем правильный тип в представление
            
           
        }

        [HttpGet]
        public ActionResult NewsUpdate(int id)
        {
            try
            {
                var news = _news.GetNewsByIdAction(id);
                if (news == null)
                {
                    TempData["Message"] = "Новость не найдена";
                    TempData["AlertType"] = "warning";
                    return RedirectToAction("NewsEditor");
                }

                return View(NewsToView(news));
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Ошибка при загрузке новости: {ex.Message}";
                TempData["AlertType"] = "danger";
                return RedirectToAction("NewsEditor");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewsUpdate(NewsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Пожалуйста, заполните все обязательные поля";
                TempData["AlertType"] = "warning";
                return View(model);
            }

            try
            {
                var news = ViewToNews(model);
                var imageFile = Request.Files["ImageFile"];

                // Если загружено новое изображение
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(imageFile.InputStream))
                    {
                        news.ImageData = binaryReader.ReadBytes(imageFile.ContentLength);
                    }
                    news.ImageMimeType = imageFile.ContentType;
                }
                else
                {
                    // Сохраняем существующее изображение
                    var existingNews = _news.GetNewsByIdAction(model.Id);
                    if (existingNews != null)
                    {
                        news.ImageData = existingNews.ImageData;
                        news.ImageMimeType = existingNews.ImageMimeType;
                    }
                }

                var isUpdated = _news.UpdateNews(news);

                TempData["Message"] = isUpdated
                    ? "Новость успешно обновлена"
                    : "Не удалось обновить новость";
                TempData["AlertType"] = isUpdated ? "success" : "danger";

                return RedirectToAction("NewsEditor");
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Ошибка при обновлении новости: {ex.Message}";
                TempData["AlertType"] = "danger";
                return View(model);
            }
        }


        // ========== USERS ===============================================================

        [HttpPost]
        public ActionResult RegisterUser(UserRegistrationData registerData)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Something went wrong";
                return RedirectToAction("AddUser");
            }

            var result = _admin.RegisterUser(registerData);
            TempData["Message"] = result.StatusMsg;

            return result.Status
                ? RedirectToAction("Clients")
                : RedirectToAction("AddUser");
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
            return user == null
                ? View("../Error/Error_500")
                : View(user);
        }

        [HttpPost]
        public ActionResult UserProfileEdit(UserInfo model)
        {
            if (!ModelState.IsValid)
                return View("ClientProfile", model);

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
            return View(new UserRegisterModel());
        }

        // ========== PRODUCTS ==========

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
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Введённые данные некорректны";
                return View(model);
            }

            // Попытка создать продукт
            var response = _product.CreateNewProduct(model);
            TempData["Message"] = response.StatusMsg;

            // В зависимости от результата, редиректим или возвращаем ту же вьюшку
            if (response.Status)
            {
                return RedirectToAction("Products");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ProductProfile(int productId)
        {
            var product = _product.GetProductById(productId);
            return product == null
                ? View("../Error/Error_500")
                : View(product);
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
                return View("ProductProfile", model);

            var product = _product.ModifyProduct(model);
            TempData["Message"] = "Изменения успешно сохранены.";
            return View("ProductProfile", product);
        }

        // ========== HELPERS ===========
        public ActionResult GetNewsImage(int id)
        {
            var news = _news.GetNewsByIdAction(id);
            if (news?.ImageData != null && news.ImageMimeType != null)
            {
                return File(news.ImageData, news.ImageMimeType);
            }
            return null;
        }
        private News ViewToNews(NewsViewModel model)
        {
            return new News
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                Author = model.Author,
                Category = model.Category,
                Tags = model.Tags,
                ImageData = model.ImageData,
                ImageMimeType = model.ImageMimeType
            };
        }

        private NewsViewModel NewsToView(News db)
        {
            return new NewsViewModel
            {
                Id = db.Id,
                Title = db.Title,
                Content = db.Content,
                Author = db.Author,
                Category = db.Category,
                Tags = db.Tags,
                ImageData = db.ImageData,
                ImageMimeType = db.ImageMimeType
            };
        }


        private List<NewsViewModel> LNewsToLView()
        {
            var newsList = _news.GetAllNews();

            if (newsList == null)
            {
                return new List<NewsViewModel>(); 
            }
            var News2 = new List<NewsViewModel>();
            foreach(var n in newsList)
            {
                var news = new NewsViewModel();
                news = NewsToView(n);
                News2.Add(news);
            }
            //var Ne = NewsToView()
            //return newsList.Select(NewsToView).ToList();
            return News2;
        }

    }
}
