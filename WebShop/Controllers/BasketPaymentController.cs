using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Order;
using WebShop.Filter;
using WebShop.Models;
using WebShop.Models.Order;

namespace WebShop.Controllers
{
    public class BasketPaymentController : Controller
    {
        IBasket _basket;
        IDelivery _delivery;
        IOrder _order;
        IUser _user;
        public BasketPaymentController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _basket = bl.GetBasketBL();
            _delivery = bl.GetDeliveryBL();
            _order = bl.GetOrderBL();
            _user = bl.GetUserBl();
        }

        [HttpPost]
        public ActionResult BasketFormHandler(string actionType, List<string> selectedProductIds, decimal orderPrice)
        {
            TempData["SelectedProductIds"] = selectedProductIds;
            TempData["OrderPrice"] = orderPrice;
            if (actionType == "remove")
            {
                // Удалить выбранные
                return RedirectToAction("RemoveSelected", "BasketPayment");
            }
            else if (actionType == "next")
            {
                // Перейти к оплате
                return RedirectToAction("Basket_step_2", "BasketPayment");
            }

            return RedirectToAction("Basket_step_1");
        }

        [HttpPost]
        public ActionResult AddToBasket(int productId, int quantity)
        {
            var user = SessionHelper.User;
            if (user == null)
                return RedirectToAction("Authorisation", "Auth");

            var response = _basket.AddToBasket(user.Id, productId, quantity);
            SessionHelper.ProductsInCartCount = _basket.GetBasketSize(user.Id);
            if(response.Status == true)
                return Json(new { success = true });
            else return Json(new { success = false });

        }

        [UserOnly]
        public ActionResult RemoveSelected()
        {
            var selectedProductIds = TempData["SelectedProductIds"] as List<string>;
            if (selectedProductIds != null)
            {
                var user = SessionHelper.User;
                var response = _basket.RemoveFromBasket(selectedProductIds, user.Id);
                if (response.Status == true)
                    SessionHelper.ProductsInCartCount = _basket.GetBasketSize(user.Id);
            }
            return RedirectToAction("Basket_step_1");
        }
        [UserOnly]
        public ActionResult Basket_step_1()
        {
            var user = SessionHelper.User;
            var products = _basket.GetAllProductsInCart(user.Id);
            int productCount = 0;
            foreach (var product in products)
            {
                productCount += product.Quantity;
            }
            if (productCount == 0)
                return RedirectToAction("Empty_basket");
            var basket = new BasketViewModel();
            basket.ProductsInBasket = new List<ProductCardViewModel>();
            foreach (var product in products)
            {
                var p = new ProductCardViewModel
                {
                    ProductName = product.Product.Name,
                    BrandName = product.Product.Producer,
                    Article = product.Product.Article,
                    Price = product.Product.Price,
                    Quantity = product.Quantity,
                    Code = product.Product.Id,
                    Status = product.Product.Status.ToString()
                    
                };
                basket.ProductsInBasket.Add(p);
                basket.TotalPrice += p.Quantity * p.Price;
                basket.ProductCount = productCount;
            }

            return View(basket);
        }
        [UserOnly]
        public ActionResult Basket_step_2()
        {
            decimal orderPrice = TempData["OrderPrice"] != null ? (decimal)TempData["OrderPrice"] : 0;
            var selectedProductIds = TempData["SelectedProductIds"] as List<string>;
            SessionHelper.ProductsSelectedForOrder = selectedProductIds;
            SessionHelper.OrderPrice = orderPrice;
            return View();
        }
        [HttpPost]
        [UserOnly]
        public ActionResult Basket_step_3(string payment, string orderMessage = "")
        {
            SessionHelper.OrderPaymentType = payment;
            SessionHelper.OrderMessage = orderMessage;
            var userId = SessionHelper.User.Id;
            var deliveryInfoDB = _delivery.GetDeliveryAddressByUserId(userId) ?? null;
            if (deliveryInfoDB != null)
            {
                var deliveryInfo = new DeliveryViewModel
                {
                    Apartment = deliveryInfoDB.Apartment,
                    Block = deliveryInfoDB.Block,
                    City = deliveryInfoDB.City,
                    House = deliveryInfoDB.House,
                    PostalCode = deliveryInfoDB.PostalCode,
                    Street = deliveryInfoDB.Street
                };
                return View(deliveryInfo);
            }
            return View(new DeliveryViewModel());
        }

        [UserOnly]
        [HttpPost]
        public ActionResult Basket_step_4(string deliveryType, string orderMessage, string userCity,
                                          string userStreet, string userHouse, string userBlock, string userAppartment)
        {
            var paymentType = SessionHelper.OrderPaymentType;
            var userId = SessionHelper.User.Id;

            var orderInfo = new OrderDTO();

            orderInfo.CreationDate = DateTime.Now.Date;
            orderInfo.EstimatedDeliveryDate = DateTime.Now.AddDays(7).Date;
            orderInfo.OrderedProducts = new List<ProductsInOrderDBTable>();

            if (paymentType == "payment-online")
                orderInfo.IsPayed = true;
            if (paymentType == "payment-cash")
                orderInfo.IsPayed = false;

            var stringList = SessionHelper.ProductsSelectedForOrder;
            var productsIdList = stringList != null
                ? stringList.Where(s => int.TryParse(s, out _)).Select(s => int.Parse(s)).ToList()
                : new List<int>();

            DeliveryViewModel deliveryLocation = new DeliveryViewModel();
            if(deliveryType == "delivery")
            {
                deliveryLocation = new DeliveryViewModel
                {
                    Apartment = userAppartment,
                    Block = userBlock,
                    City = userCity,
                    Street = userStreet,
                    House = userHouse
                };
                orderInfo.IsPickup = false;
                SessionHelper.Delivery = deliveryLocation;
            }
            if(deliveryType == "pickup")
            {
                orderInfo.IsPickup = true;
            }
            orderInfo.Price = SessionHelper.OrderPrice;
            orderInfo.Status = Domain.Enumerables.OrderStatus.Pending;
            orderInfo.Comment = orderMessage;
            var response = _order.CreateNewOrder(orderInfo, userId, productsIdList);
            var order = _order.GetOrderById(response.OrderId);
            var model = new OrderModel
            {
                Comment = order.Comment,
                OrderId = order.Id,
                DeliveryCity = deliveryLocation.City,
                EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                isPaid = order.IsPayed,
                Price = order.Price,
                DeliveryType = deliveryType,
            };
            SessionHelper.ProductsInCartCount = _basket.GetBasketSize(userId);
            return View(model);
        }
        [UserOnly]
        public ActionResult Empty_basket()
        {
            return View();
        }
        [HttpGet]
        [UserOnly]
        public ActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Payment(string userEmail, decimal userPrice = 0)
        {
            bool paymentSuccess = true;
            if (string.IsNullOrEmpty(userEmail) || userPrice <= 0)
            {
                ViewBag.PaymentError = "Неверные данные для оплаты.";
                paymentSuccess = false;
                return View();
            }

            if (paymentSuccess)
            {
                ViewBag.PaymentSuccess = true;
            }
            else
            {
                ViewBag.PaymentError = "Не удалось обработать платёж. Попробуйте ещё раз.";
            }

            return View();
        }
        [HttpPost]
        [UserOnly]
        public ActionResult SupplyBalance (decimal moneyToAdd)
        {
           var response  = _user.SupplyBalance(SessionHelper.User.Id, moneyToAdd);
           SessionHelper.User = _user.GetUserInfoById(SessionHelper.User.Id);
           return RedirectToAction("Payment"); 
        }
    }
}