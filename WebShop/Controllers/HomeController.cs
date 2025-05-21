using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Product;
using WebShop.Models;
using WebShop.Models.MainPageModels;

namespace WebShop.Controllers
{
    public class HomeController : BaseController
    {
        IProduct _product;
        //INews _news;
        public HomeController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _product = bl.GetProductBl();
            //_news = bl.GetNewsBl();
        }
        public ActionResult Index()
        {
            //Не используется, поэтому Redirect
            //return View();
            return RedirectPermanent("MainPage");
        }

        public ActionResult MainPage()
        {
            MainPageModel model = new MainPageModel();
            //model.News = _news.GetAllNews();
            // TO DO. //
            // Добавить передачу в модель списка новостей для отображения на главной странице

            var bonusProducts = _product.GetProductsByStatus("bonus", 1, 8, 0m, int.MaxValue, true, null);
            var productsForView = new List<ProductCardViewModel>();
            foreach (ProductDTO p in bonusProducts.products)
            {
                var product = new ProductCardViewModel
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
                };
                productsForView.Add(product);
            }
            
            model.SearchResults = productsForView;
            return View(model);
        }
    }
}