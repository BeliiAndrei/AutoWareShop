using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Enumerables;
using WebShop.Domain.News;
using WebShop.Domain.Product;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Registration;

using WebShop.Filters;
using WebShop.Models;
using WebShop.Models.Order;
using WebShop.Models.Search;
using WebShop.Models.User;

namespace WebShop.Controllers
{
    [AdminOnly]
    public class AdminController : Controller
    {
        private readonly IAdmin _admin;
        private readonly IProduct _product;
        private readonly INews _news;
        private readonly IOrder _order;
        private readonly IDelivery _delivery;
        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _admin = bl.GetAdminBl();
            _product = bl.GetProductBl();
            _news = bl.GetNewsBl();
            _order = bl.GetOrderBL();
            _delivery = bl.GetDeliveryBL();
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
        public ActionResult RegisterUser(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Something went wrong (Model is not valid). Убедитесь, что данные удовлетворяют требованиям";
                return RedirectToAction("AddUser");
            }
            if(model.Password != model.RePassword)
            {
                TempData["Message"] = "Повторный пароль введён неправильно";
                return RedirectToAction("AddUser");
            }
            var registerData = new UserRegistrationData
            {
                Balance = model.Balance,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                Role = (UserRole)Enum.Parse(typeof(UserRole), model.Role),
                UserName = model.UserName,
                UserLastName = model.UserLastName
            };
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
            var model = new UserInfoModel
            {
                UserName = user.UserName,
                UserLastName = user.UserLastName,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
                Email = user.Email,
                Balance = user.Balance,
                Role = user.Role
            };
            return user == null
                ? View("../Error/Error_404")
                : View(model);
        }

        [HttpPost]
        public ActionResult UserProfileEdit(UserInfoModel model)
        {
            if (!ModelState.IsValid)
                return View("ClientProfile", model);

            var userInfo = new UserInfo
            {
                UserName = model.UserName,
                Email = model.Email,
                Balance = model.Balance,
                Id = model.Id,
                PhoneNumber = model.PhoneNumber,
                Role = model.Role,
                UserLastName = model.UserLastName
            };
            var user = _admin.UpdateUser(userInfo);
            var editedModel = new UserInfoModel
            {
                UserLastName = user.UserLastName,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
                Balance = user.Balance,
                Email = user.Email,
                UserName = user.UserName
            };
            SessionHelper.User = user;
            TempData["SuccessMessage"] = "Изменения успешно сохранены.";
            return View("ClientProfile", editedModel);
        }

        public ActionResult Clients()
        {
            var users = _admin.GetUsersList();
            var model = new List<UserInfoModel>();
            foreach (var u in users)
            {
                var m = new UserInfoModel
                {
                    Balance = u.Balance,
                    Email = u.Email,
                    Id = u.Id,
                    PhoneNumber = u.PhoneNumber,
                    Role = u.Role,
                    UserLastName = u.UserLastName,
                    UserName = u.UserName
                };
                model.Add(m);
            }
            return View(model);
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
            if (!ModelState.IsValid)
            {
                
            TempData["Message"] = "Введённые данные некорректны";
                return View(model);
            }

            var response = _product.CreateNewProduct(model);
            TempData["Message"] = response.StatusMsg;

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

        // ========== Orders ============

        public ActionResult Orders(int page = 1)
        {
            int pageSize = 10;
            var response = _order.GetAllOrders(page, pageSize);
            var model = new AdminOrdersModel
            {
                Page = new PageInfo(response.ordersTotalCount, page, pageSize),
                TotalCount = response.ordersTotalCount
            };
            model.Orders = new List<OrderModel>();
            foreach (var order in response.orders)
            {
                var o = new OrderModel
                {
                    Comment = order.Comment,
                    Created = order.CreationDate,
                    EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                    isPaid = order.IsPayed,
                    OrderStatus = order.Status,
                    OrderId = order.Id,
                    Price = order.Price,
                    UserId = order.UserId
                };
                model.Orders.Add(o);
            };
            return View(model);
        }

        public ActionResult OrderDetails(int orderId)
        {
            var order = _order.GetOrderById(orderId);
            var orderModel = new OrderModel
            {
                OrderId = orderId,
                UserId = order.UserId,
                Comment = order.Comment,
                Created = order.CreationDate,
                EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                isPaid = order.IsPayed,
                OrderStatus = order.Status,
                Price = order.Price
            };
            if (order.Status == Domain.Enumerables.OrderStatus.Received)
                orderModel.isReceived = true;
            else orderModel.isReceived = false;
            if (order.IsPickup)
                orderModel.DeliveryType = "Самовывоз";
            else orderModel.DeliveryType = "Доставка";
            var products = new List<ProductCardViewModel>();
            foreach (var p in order.OrderedProducts)
            {
                var productFromDB = _product.GetProductById(p.ProductId);
                var productModel = new ProductCardViewModel
                {
                    Article = productFromDB.Article,
                    BrandName = productFromDB.Producer,
                    Code = productFromDB.Id,
                    Description = productFromDB.Description,
                    Price = productFromDB.Price,
                    ProductName = productFromDB.Name,
                    Quantity = p.Quantity,
                };
                products.Add(productModel);
            }
            orderModel.Products = products;
            var user = _admin.GetUserById(orderModel.UserId);
            DeliveryViewModel delivery = null;
            if(order.IsPickup == false)
            {
                var response = _delivery.GetDeliveryAddressByUserId(user.Id);
                delivery = new DeliveryViewModel {
                    City = response.City,
                    Street = response.Street,
                    House = response.House,
                    Block = response.Block,
                    Apartment = response.Apartment,
                    Comment = response.Comment,
                    PostalCode = response.PostalCode
                };
            }
            var userInfo = new UserInfoModel
            {
                UserName = user.UserName,
                Balance = user.Balance,
                Email = user.Email,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                UserLastName = user.UserLastName
            };
            var orderDetailsOrder = new OrderDetailsModel
            {
                userInfo = userInfo,
                orderModel = orderModel,
                deliveryInfo = delivery
            };
            return View(orderDetailsOrder);
        }

        [HttpPost]
        public ActionResult UpdateOrderStatus(int orderId, OrderStatus newStatus)
        {
            _ = _order.UpdateOrder(orderId, newStatus);
            return RedirectToAction("OrderDetails", new { orderId });
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
            foreach (var n in newsList)
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
