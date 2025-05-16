using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Models;


namespace WebShop.Controllers
{

    public class ProductController : BaseController
    {
        IProduct _product;
        public ProductController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _product = bl.GetProductBl();
        }
        public ActionResult Card(string value)
        {
            var item = _product.GetProductByArticle(value);
            if (item == null || item.Status == Domain.Enumerables.ProductStatus.hidden)
                return RedirectToAction("Error_404", "Error");
            ProductCardViewModel product = new ProductCardViewModel();
            product.Article = value;
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