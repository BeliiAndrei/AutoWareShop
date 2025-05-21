using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Order;
using WebShop.Domain.User.Delivery;
using WebShop.Filter;
using WebShop.Models;
using WebShop.Models.Order;
using WebShop.BusinessLogic.BLogic;

namespace WebShop.Controllers
{
    public class BasketPaymentController : BaseController
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
            if (payment == "payment-online" && SessionHelper.User.Balance >= SessionHelper.OrderPrice)
            {
                //Тут списание со счёта, если на нём достаточно средств (параметр со знаком минус)
                _user.SupplyBalance(SessionHelper.User.Id, -(SessionHelper.OrderPrice));
                SessionHelper.User = _user.GetUserInfoById(SessionHelper.User.Id);
            }
            else
            {
                if (payment == "payment-online" && SessionHelper.User.Balance < SessionHelper.OrderPrice)
                {
                    TempData["NotEnoughMoney"] = $"У вас недостаточно средств на счету. У вас на счету {SessionHelper.User.Balance.ToString("F2") ?? "0.00"}, а стоимость заказа {SessionHelper.OrderPrice}.";
                    return RedirectToAction("Payment");
                }
            }
            SessionHelper.OrderMessage = orderMessage;
            var userId = SessionHelper.User.Id;
            var deliveryInfoDB = _delivery.GetDeliveryAddressesByUserId(userId) ?? null;//.GetDeliveryAddressByUserId(userId) ?? null;
            if (deliveryInfoDB != null)
            {
                List<DeliveryViewModel> allAdresses = new List<DeliveryViewModel>();
                foreach(var adress in deliveryInfoDB)
                {
                    var deliveryInfo = new DeliveryViewModel
                    {
                        Apartment = adress.Apartment,
                        Block = adress.Block,
                        City = adress.City,
                        House = adress.House,
                        PostalCode = adress.PostalCode,
                        Street = adress.Street
                    };
                    allAdresses.Add(deliveryInfo);
                }
                return View(allAdresses);
            }
            return View(new DeliveryViewModel());
        }

        [UserOnly]
        [HttpPost]
        public ActionResult Basket_step_4(string deliveryType, string orderMessage, List<DeliveryViewModel> Addresses, int selectedAddressIndex)
        {
            var selectedAddress = Addresses[selectedAddressIndex];
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
            if (deliveryType == "delivery")
            {
                deliveryLocation = new DeliveryViewModel
                {
                    Apartment = selectedAddress.Apartment,
                    Block = selectedAddress.Block,
                    City = selectedAddress.City,
                    Street = selectedAddress.Street,
                    House = selectedAddress.House
                };
                orderInfo.IsPickup = false;
                SessionHelper.Delivery = deliveryLocation;
            }
            if (deliveryType == "pickup")
            {
                orderInfo.IsPickup = true;
            }
            orderInfo.Price = SessionHelper.OrderPrice;
            orderInfo.Status = Domain.Enumerables.OrderStatus.Pending;
            orderInfo.Comment = orderMessage;
            var response = _order.CreateNewOrder(orderInfo, userId, productsIdList);
            var order = _order.GetOrderById(response.OrderId);

            if (response.Status == false)
            {
                var errorResponse = _order.ModifyOrderStatus(order.Id, Domain.Enumerables.OrderStatus.Cancelled);
                if (errorResponse.Status == false)
                {
                    return RedirectToAction("OrderError", "Error", new { message = errorResponse.StatusMsg + "Обязательно свяжитесь с нами, если произошла ошибка, приведшая к потере средств." });
                }
                if (order.IsPayed)
                {
                    _user.SupplyBalance(userId, order.Price);
                    SessionHelper.User = _user.GetUserInfoById(SessionHelper.User.Id);
                    response.StatusMsg += " Средства были возвращены на ваш счёт. В случае обнаружения ошибок, пожалуйста, свяжитесь с нами.";
                }
                order = _order.UpdateOrderPrice(order.Id, 0m);
                SessionHelper.ProductsInCartCount = _basket.GetBasketSize(userId);
                return RedirectToAction("OrderError", "Error", new { message = response.StatusMsg });
            }
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