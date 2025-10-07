using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.News;
using WebShop.Domain.ResBook;
using WebShop.Filters;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class InformationalController : BaseController
    {
        INews _news;
        IResBook _resBook;

        public InformationalController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _news = bl.GetNewsBl();
            _resBook = bl.GetResBookBl();
        }

        // ========== СУЩЕСТВУЮЩИЕ МЕТОДЫ ==========

        public ActionResult About_1()
        {
            return View();
        }

        public ActionResult About_2()
        {
            return View();
        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Vacation()
        {
            return View();
        }

        public ActionResult ViewNews()
        {
            List<NewsViewModel> newsList = LNewsToLView();
            return View(newsList);
        }

        // ========== НОВЫЕ МЕТОДЫ ДЛЯ БРОНИРОВАНИЯ КНИГ ==========

        // GET: Страница бронирования книги
        public ActionResult ReserveBook()
        {
            return View(new ResBook());
        }

        // POST: Обработка формы бронирования
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReserveBook(ResBook model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Если валидация не прошла, возвращаем форму с ошибками
                    return View(model);
                }

                // Создаем бронирование
                var result = _resBook.CreateReservation(model);

                if (result)
                {
                    TempData["SuccessMessage"] = "✅ Ваша заявка на бронирование книги успешно отправлена! Мы свяжемся с вами в течение 24 часов.";

                    // Очищаем модель после успешной отправки
                    ModelState.Clear();
                    return View(new ResBook());
                }
                else
                {
                    ModelState.AddModelError("", "❌ Произошла ошибка при отправке заявки. Пожалуйста, попробуйте еще раз.");
                    return View(model);
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при создании бронирования: {ex}");
                ModelState.AddModelError("", "❌ Произошла непредвиденная ошибка. Пожалуйста, попробуйте позже.");
                return View(model);
            }
        }

        // GET: Просмотр всех бронирований (для админа)
        [AdminOnly] // если есть такой атрибут
        public ActionResult ViewReservations()
        {
            try
            {
                var reservations = _resBook.GetAllReservations();
                return View(reservations);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при загрузке бронирований: {ex}");
                TempData["ErrorMessage"] = "Ошибка при загрузке списка бронирований";
                return View(new List<ResBook>());
            }
        }

        // GET: Детали бронирования
        [AdminOnly]
        public ActionResult ReservationDetails(int id)
        {
            try
            {
                var reservation = _resBook.GetReservationById(id);
                if (reservation == null)
                {
                    TempData["ErrorMessage"] = "Бронирование не найдено";
                    return RedirectToAction("ViewReservations");
                }
                return View(reservation);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при загрузке бронирования: {ex}");
                TempData["ErrorMessage"] = "Ошибка при загрузке данных бронирования";
                return RedirectToAction("ViewReservations");
            }
        }

        // POST: Удаление бронирования
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult DeleteReservation(int id)
        {
            try
            {
                var result = _resBook.DeleteReservation(id);

                if (result)
                {
                    TempData["SuccessMessage"] = "Бронирование успешно удалено";
                }
                else
                {
                    TempData["ErrorMessage"] = "Не удалось удалить бронирование";
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при удалении бронирования: {ex}");
                TempData["ErrorMessage"] = "Ошибка при удалении бронирования";
            }

            return RedirectToAction("ViewReservations");
        }

        // GET: Поиск бронирований по email
        [AdminOnly]
        public ActionResult SearchReservations(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return RedirectToAction("ViewReservations");
                }

                var reservations = _resBook.GetReservationsByEmail(email);
                ViewBag.SearchEmail = email;
                return View("ViewReservations", reservations);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при поиске бронирований: {ex}");
                TempData["ErrorMessage"] = "Ошибка при поиске бронирований";
                return RedirectToAction("ViewReservations");
            }
        }

        // ========== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ==========

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

            return News2;
        }
    }
}