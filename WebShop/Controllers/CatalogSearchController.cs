using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Product;
using WebShop.Domain.Product.SearchResponses;
using WebShop.Models;
using WebShop.Models.Search;

namespace WebShop.Controllers
{
    public class CatalogSearchController : Controller
    {
        IProduct _product;
        public CatalogSearchController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _product = bl.GetProductBl();
        }
        public ActionResult Catalog()
        {
            return View();
        }

        public ActionResult Catalog_brands()
        {
            return View();
        }

        public ActionResult Match_vincode()
        {
            return View();
        }
        
        public ActionResult SearchMethod(string searchType, string value)
        {
            if (searchType == "article")
                return RedirectToAction("Card", "Product", new {value});
            if (searchType == "name")
                return RedirectToAction("Search_result", new { Result_SearchString = value });
            return RedirectToAction("Error_500", "Error");
        }

        public ActionResult Search_result(string Path_Category, string Path_Subcategory, string Result_SearchString = "", int page = 1, decimal minPrice = 0, decimal maxPrice = 10000, bool onlyAvailable = false, List<string> brands = null)
        {
            var pageSize = 8;   // РАЗМЕР СТРАНИЦЫ ТУТ
            SearchModel viewModel;
            ProductSearchResponseDTO result;
            if (!string.IsNullOrEmpty(Path_Category) || !string.IsNullOrEmpty(Path_Subcategory))
            {
                result = _product.GetProductsByCategory(Path_Subcategory, page, pageSize, minPrice, maxPrice, onlyAvailable, brands);
                viewModel = FormViewModel(result, pageSize, page, Result_SearchString);
                viewModel.Path = new SearchPath
                {
                    Category = Path_Category,
                    Subcategory = Path_Subcategory
                };
                return View(viewModel);
            }
            if(Result_SearchString !="")
            {
                if (Result_SearchString == "bonus")
                    result = _product.GetProductsByStatus(Result_SearchString, page, pageSize, minPrice, maxPrice, onlyAvailable, brands);
                else
                    result = _product.GetProductsBySearchString(Result_SearchString, page, pageSize, minPrice, maxPrice, onlyAvailable, brands);
                viewModel = FormViewModel(result, pageSize, page, Result_SearchString);
                return View(viewModel);
            }
            return RedirectToAction("Error_500", "Error");
        }

        internal SearchModel FormViewModel(ProductSearchResponseDTO response, int pageSize, int page = 1, string Result_SearchString = "")
        {
            var productsForView = new List<ProductCardViewModel>();
            foreach (ProductDTO p in response.products)
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
            SearchResult searchResult = new SearchResult
            {
                Products = productsForView,
                SearchString = Result_SearchString,
                TotalCount = response.productsTotalCount,
                AvailableBrands = response.brands
            };
            PageInfo pageInfo = new PageInfo(searchResult.TotalCount, page, pageSize);
            SearchModel viewModel = new SearchModel(searchResult, pageInfo);
            //viewModel.Result.AvailableBrands = new List<string> { "Filtron", "Bosh" };  // Тест!!!!
            return viewModel;
        }

        public ActionResult Select_car()
        {
            return View();
        }
    }
}