using System.Collections.Generic;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Modify;
using WebShop.Filter;
using WebShop.Models;
using WebShop.Models.Order;
using WebShop.Models.User;

namespace WebShop.Controllers
{

    [UserOnly]
    public class ProfileController : BaseController
    {
        IUser _user;
        IOrder _order;
        IProduct _product;

        public ProfileController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _user = bl.GetUserBl();
            _order = bl.GetOrderBL();
            _product = bl.GetProductBl();
        }
        public ActionResult Orders()
        {
            var user = SessionHelper.User;
            var userOrders = _order.GetUserOrders(user.Id);
            var model = new List<OrderModel>();
            foreach (var order in userOrders)
            {
                var o = new OrderModel()
                {
                    Comment = order.Comment,
                    Created = order.CreationDate,
                    OrderId = order.Id,
                    EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                    isPaid = order.IsPayed,
                    Price = order.Price,
                    OrderStatus = order.Status
                };
                if (order.IsPickup)
                    o.DeliveryType = "Самовывоз";
                if (!order.IsPickup)
                    o.DeliveryType = "Доставка";
                if (order.Status == Domain.Enumerables.OrderStatus.Received)
                    o.isReceived = true;
                else o.isReceived = false;
                o.DeliveryCity = SessionHelper.Delivery?.City ?? "Chisinau";
                model.Add(o);
            }
            return View(model);
        }

        public ActionResult OrderInfo(int orderId)
        {
            var order = _order.GetOrderById(orderId);
            var orderModel = new OrderModel
            {
                OrderId = orderId,
                Comment = order.Comment,
                Created = order.CreationDate,
                EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                isPaid = order.IsPayed,
                Price = order.Price,
                OrderStatus = order.Status,
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
                    Quantity = p.Quantity
                };
                products.Add(productModel);
            }
            orderModel.Products = products;
            return View(orderModel);
        }

        public ActionResult ProfileUser()
        {
            var user = SessionHelper.User;
            var model = new UserInfoModel
            {
                UserName = user.UserName,
                UserLastName = user.UserLastName,
                Role = user.Role,
                Balance = user.Balance,
                Email = user.Email,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber
            };
            return View(model);
        }


        public ActionResult SaveProfile(UserInfoModel model)
        {

            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Введённые данные некорректны";
                return View("ClientProfile", model);
            }
            var data = new UserInfo
            {
                UserName = model.UserName,
                Balance = model.Balance,
                Email = model.Email,
                Id = model.Id,
                PhoneNumber = model.PhoneNumber,
                Role = model.Role,
                UserLastName = model.UserLastName
            };
            var user = _user.EditUserProfile(data);
            TempData["Message"] = "Изменения успешно сохранены.";
            SessionHelper.User = user;
            var editedModel = new UserInfoModel
            {
                UserLastName = user.UserLastName,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
                Email = user.Email,
                Balance = user.Balance,
                UserName = user.UserName
            };
            return View("ProfileUser", editedModel);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel passwordModel)
        {
            UserInfo user = SessionHelper.User;
            passwordModel.Id = user.Id;
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Некорректны введённые данные данные";
                return RedirectToAction("ProfileUser");
            }
            if (passwordModel.NewPassword != passwordModel.ConfirmPassword)
            {
                TempData["Message"] = "Новый пароль не совпадает";
                return RedirectToAction("ProfileUser");
            }

            var success = _user.ChangePasswordInDB(new ChangePasswordClass
            {
                Id = passwordModel.Id,
                OldPassword = passwordModel.OldPassword,
                NewPassword = passwordModel.NewPassword
            }
            );
            if (!success)
            {
                TempData["Message"] = "Что-то пошло не так. Убедитесь что введённый пароль корректен";
                return RedirectToAction("ProfileUser");
            }
            TempData["Message"] = "Пароль успешно изменён.";
            return RedirectToAction("ProfileUser");
        }
    }
}