using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.News;
using WebShop.Domain.Product;
using WebShop.Models;
using WebShop.Models.MainPageModels;

namespace WebShop.Controllers
{
    public class HomeController : BaseController
    {
        IProduct _product;
        INews _news;
        public HomeController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _product = bl.GetProductBl();
            _news = bl.GetNewsBl();
        }
        public ActionResult Index()
        {
            private readonly IProduct _product;
            private readonly INews _news;

            public HomeController()
            {
                var bl = new BusinessLogic.BusinessLogic();
                _product = bl.GetProductBl();
                _news = bl.GetNewsBl();
            }

            public ActionResult Index()
            {
                return RedirectPermanent("MainPage");
            }

            public ActionResult MainPage()
        {
            MainPageModel model = new MainPageModel();
            var newsDTO = _news.GetAllNews();
            model.News = LNewsToLView();

            var bonusProducts = _product.GetProductsByStatus("bonus", 1, 8, 0m, int.MaxValue, true, null);
            var productsForView = new List<ProductCardViewModel>();
            foreach (ProductDTO p in bonusProducts.products)
            {
                var model = new MainPageModel
                {
                    // Достаем все новости подряд, без сортировки и ограничений
                    News = _news.GetAllNews(),

                    // Достаем товары (как было в оригинале)
                    SearchResults = GetBonusProducts()
                };

                return View(model);
            }

            private List<ProductCardViewModel> GetBonusProducts()
            {
                var bonusProducts = _product.GetProductsByStatus("bonus", 1, 8, 0m, int.MaxValue, true, null);
                var productsForView = new List<ProductCardViewModel>();

                foreach (var p in bonusProducts.products)
                {
                    productsForView.Add(new ProductCardViewModel
                    {
                        ProductName = p.Name,
                        BrandName = p.Producer,
                        Code = p.Id,
                        Article = p.Article,
                        Description = p.Description,
                        Image = p.ImageNumber,
                        Price = p.Price,
                        Quantity = p.Quantity,
                        Status = p.Status.ToString()
                    });
                }

                return productsForView;
            }
            public FileContentResult GetNewsImage(int newsId)
            {
                var news = _news.GetNewsByIdAction(newsId);
                if (news?.ImageData != null)
                {
                    return File(news.ImageData, news.ImageMimeType ?? "image/jpeg");
                }
                // Заглушка, если нет изображения
                var defaultImagePath = Server.MapPath("~/assets/images/content/news-default.jpg");
                var imageBytes = System.IO.File.ReadAllBytes(defaultImagePath);
                return File(imageBytes, "image/jpeg");
            }
        }
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

