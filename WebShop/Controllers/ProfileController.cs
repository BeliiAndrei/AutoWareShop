using System.Collections.Generic;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Modify;
using WebShop.Filter;
using WebShop.Models;
using WebShop.Models.Order;

namespace WebShop.Controllers
{

    [UserOnly]
    public class ProfileController : Controller
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
            var user = Session["User"] as UserInfo;
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
                if(order.Status == Domain.Enumerables.OrderStatus.Received)
                    o.isReceived = true;
                else o.isReceived = false;
                o.DeliveryCity = (Session["Delivery"] as DeliveryViewModel)?.City ?? "Chisinau";
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
            if(order.Status == Domain.Enumerables.OrderStatus.Received)
                orderModel.isReceived = true;
            else orderModel.isReceived = false;
            if (order.IsPickup)
                orderModel.DeliveryType = "Самовывоз";
            else orderModel.DeliveryType = "Доставка";
            var products = new List<ProductCardViewModel>();
            foreach(var p in order.OrderedProducts)
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
            var user = Session["User"];
            return View(user);
        }

      
        public ActionResult SaveProfile(UserInfo edited)
        {
           
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = "Введённые данные некорректны";
                    return View("ClientProfile", edited);
                }
                var user = _user.EditUserProfile(edited);
                TempData["Message"] = "Изменения успешно сохранены.";
                Session["User"] = user;
                return View("ProfileUser", user); 
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel passwordModel)
        {
            UserInfo user = (UserInfo)Session["User"];
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
                {Id = passwordModel.Id, 
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