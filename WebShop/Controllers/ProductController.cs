using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Models;


namespace WebShop.Controllers
{

    public class ProductController : Controller
    {
        IProduct _product;
        public ProductController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _product = bl.GetProductBl();
        }
        // GET: Product
        public ActionResult Card(string article)
        {
            var item = _product.GetProductByArticle(article);
            if (item == null)
                return RedirectToAction("Error_404", "Error");
            ProductCardViewModel product = new ProductCardViewModel();
            product.Article = article;
            product.ProductName = item.Name;
            product.BrandName = item.Producer;
            product.Image = item.ImageNumber;
            product.Price = item.Price;
            product.Description = item.Description;
            product.Quantity = item.Quantity;
            product.Code = item.Id;
            return View(product);
        }
    }
}