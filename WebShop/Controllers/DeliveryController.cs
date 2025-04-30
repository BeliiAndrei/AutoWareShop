using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Delivery;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class DeliveryController : Controller
    {
        //==============================Delivery=========================================
        
        //!!! Перенёс из Auth в отдельный контроллер как мы говорили. Тут почему-то не нахожу EditDelivery. Надо бы добавить.


        private readonly IDelivery _delivery;
        public DeliveryController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _delivery = bl.GetDeliveryBL();
        }
        public ActionResult Delivery()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delivery(DeliveryViewModel deliveryData)
        {
            if (ModelState.IsValid)
            {
                var user = SessionHelper.User;
                if (user != null)
                {
                    deliveryData.UserId = user.Id;
                    DeliveryL delivery = ViewToL(deliveryData);

                    var deliveryResponse = _delivery.AddDeliveryAddress(delivery);
                }
                TempData["Message"] = "Succes";
                return RedirectToAction("ProfileUser", "Profile");

            }
            TempData["Message"] = "Something went wrong";
            return View("DeliveryCreate");
        }


        public ActionResult DeliveryList()
        {
            var user = SessionHelper.User;
            if (user == null)
            {
                TempData["Message"] = "Для просмотра адресов доставки необходимо авторизоваться";
                TempData["AlertType"] = "danger";
                return RedirectToAction("Authorisation", "Auth");
            }

            try
            {
                var deliveryAddresses = _delivery.GetDeliveryAddressesByUserId(user.Id);
                var viewModels = deliveryAddresses.Select(LToView).ToList();

                return View(viewModels);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                TempData["Message"] = "Произошла ошибка при загрузке адресов доставки";
                TempData["AlertType"] = "danger";
                return View(new List<DeliveryViewModel>());
            }
        }

        public DeliveryL ViewToL(DeliveryViewModel view)
        {
            return new DeliveryL
            {
                UserId = view.UserId,
                PostalCode = view.PostalCode,
                City = view.City,
                Street = view.Street,
                House = view.House,
                Block = view.Block,
                Apartment = view.Apartment,
                Comment = view.Comment
            };
        }

        public DeliveryViewModel LToView(DeliveryL delivery)
        {
            return new DeliveryViewModel
            {
                UserId = delivery.UserId,
                PostalCode = delivery.PostalCode,
                City = delivery.City,
                Street = delivery.Street,
                House = delivery.House,
                Block = delivery.Block,
                Apartment = delivery.Apartment,
                Comment = delivery.Comment
            };
        }


        //==============================End_Delivery=====================================
    }
}