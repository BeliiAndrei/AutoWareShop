using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;


namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Card()
        {
            ProductCardViewModel item = new ProductCardViewModel();
            item.Quantity = 10;
            item.Price = 500;
            item.ProductName = "Фильтр масляный" ;
            item.BrandName = "Filtron";
            item.Article = "OP525";
            item.Code = 465907;
            return View(item);
        }
    }
}