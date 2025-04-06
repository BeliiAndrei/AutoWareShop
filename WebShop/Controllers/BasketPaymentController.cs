using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Product;
using WebShop.Domain.User.Admin;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class BasketPaymentController : Controller
    {
        IBasket _basket;
        public BasketPaymentController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _basket = bl.GetBasketBL();
        }

        [HttpPost]
        public ActionResult AddToBasket(int productId, int quantity)
        {
            var user = Session["User"] as UserInfo;
            if (user == null)
                return RedirectToAction("Authorisation", "Auth");

            var response = _basket.AddToBasket(user.Id, productId, quantity);
            Session["BasketCount"] = _basket.GetBasketSize(user.Id);
            if(response.Status == true)
                return Json(new { success = true });
            else return Json(new { success = false });

        }

        
        public ActionResult RemoveSelected(List<string> selectedProductIds)
        {
            if (selectedProductIds != null)
            {
                var user = Session["User"] as UserInfo;
                var response = _basket.RemoveFromBasket(selectedProductIds, user.Id);
                if (response.Status == true)
                    Session["BasketCount"] = _basket.GetBasketSize(user.Id);
            }
            return RedirectToAction("Basket_step_1");
        }
        public ActionResult Basket_step_1()
        {
            var user = Session["User"] as UserInfo;
            if (user == null)
                return RedirectToAction("Authorisation", "Auth");
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
                    Code = product.Product.Id
                    
                };
                basket.ProductsInBasket.Add(p);
                basket.TotalPrice += p.Quantity * p.Price;
                basket.ProductCount = productCount;
            }

            return View(basket);
        }
        public ActionResult Basket_step_2()
        {
            return View();
        }
        public ActionResult Basket_step_3()
        {
            return View();
        }

        public ActionResult Basket_step_4()
        {
            return View();
        }

        public ActionResult Empty_basket()
        {
            return View();
        }
        public ActionResult Payment()
        {
            return View();
        }
    }
}