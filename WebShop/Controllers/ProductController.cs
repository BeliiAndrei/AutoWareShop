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
            item.Image = "1";
            item.Description = "Ма́сляный фи́льтр — устройство, предназначенное для удаления загрязнений " +
                "из моторных, компрессорных, турбинных, трансмиссионных, смазочных масел, гидравлических жидкостей (жидкость для автоматической коробки перемены передач, жидкость для гидравлического усилителя рулевого управления) и др.";
            return View(item);
        }
    }
}