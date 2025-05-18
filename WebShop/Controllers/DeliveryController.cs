using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Delivery;
using WebShop.Filter;
using WebShop.Models;

namespace WebShop.Controllers
{
    [UserOnly]
    public class DeliveryController : Controller
    {

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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDelivery(int id)
        {
            var user = SessionHelper.User;
            if (user == null)
            {
                TempData["Message"] = "Для удаления адреса доставки необходимо авторизоваться";
                TempData["AlertType"] = "danger";
                return RedirectToAction("Authorisation", "Auth");
            }

            try
            {
                // Проверяем, что адрес принадлежит пользователю
                var addresses = _delivery.GetDeliveryAddressesByUserId(user.Id);
                if (!addresses.Any(a => a.Id == id))
                {
                    TempData["Message"] = "Адрес не найден или не принадлежит вам";
                    TempData["AlertType"] = "danger";
                    return RedirectToAction("DeliveryList");
                }

                var response = _delivery.DeleteDeliveryAddress(id);

                if (response)
                {
                    TempData["Message"] = "Адрес доставки успешно удалён";
                    TempData["AlertType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Не удалось удалить адрес доставки";
                    TempData["AlertType"] = "danger";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                TempData["Message"] = "Произошла ошибка при удалении адреса доставки";
                TempData["AlertType"] = "danger";
            }

            return RedirectToAction("DeliveryList");
        }

        [HttpGet]
        public ActionResult EditDelivery(int id)
        {
            var user = SessionHelper.User;
            if (user == null)
            {
                TempData["Message"] = "Для редактирования адреса необходимо авторизоваться";
                TempData["AlertType"] = "danger";
                return RedirectToAction("Authorisation", "Auth");
            }

            try
            {
                var addresses = _delivery.GetDeliveryAddressesByUserId(user.Id);
                var address = addresses.FirstOrDefault(a => a.Id == id);

                if (address == null)
                {
                    TempData["Message"] = "Адрес не найден";
                    TempData["AlertType"] = "danger";
                    return RedirectToAction("DeliveryList");
                }

                return View(LToView(address));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                TempData["Message"] = "Ошибка при загрузке адреса";
                TempData["AlertType"] = "danger";
                return RedirectToAction("DeliveryList");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDelivery(DeliveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = SessionHelper.User;
            if (user == null)
            {
                TempData["Message"] = "Для редактирования адреса необходимо авторизоваться";
                TempData["AlertType"] = "danger";
                return RedirectToAction("Authorisation", "Auth");
            }

            try
            {
                var address = ViewToL(model);
                var result = _delivery.EditDeliveryAddress(address, user.Id);

                if (result)
                {
                    TempData["Message"] = "Адрес успешно обновлён";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("DeliveryList");
                }

                TempData["Message"] = "Ошибка при обновлении адреса";
                TempData["AlertType"] = "danger";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                TempData["Message"] = "Ошибка при обновлении адреса";
                TempData["AlertType"] = "danger";
            }

            return View(model);
        }
        public DeliveryL ViewToL(DeliveryViewModel view)
        {
            return new DeliveryL
            {
                Id = view.Id,
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
                Id = delivery.Id,
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


    }
}