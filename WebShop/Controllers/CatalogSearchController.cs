﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BusinessLogic;
using WebShop.BusinessLogic.BLogic;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Product;
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

        public ActionResult Search_result(string Path_Category, string Path_Subcategory, string Result_SearchString = "", int page = 1)
        {
            SearchModel viewModel;
            List<ProductDTO> result;
            if (Path_Category != null || Path_Subcategory != null)
            {
                result = _product.GetProductsByCategory(Path_Subcategory);
                viewModel = FormViewModel(result, page, Result_SearchString);
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
                    result = _product.GetProductsByStatus(Result_SearchString);
                else
                    result = _product.GetProductsBySearchString(Result_SearchString);
                viewModel = FormViewModel(result, page, Result_SearchString);
                return View(viewModel);
            }
            return RedirectToAction("Error_500", "Error");
        }

        internal SearchModel FormViewModel(List<ProductDTO> products, int page = 1, string Result_SearchString = "")
        {
            var productsForView = new List<ProductCardViewModel>();
            foreach (ProductDTO p in products)
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
                TotalCount = products.Count()
            };
            PageInfo pageInfo = FormPageInfo(searchResult, page);
            SearchModel viewModel = new SearchModel(searchResult, pageInfo);
            return viewModel;
        }

        internal PageInfo FormPageInfo(SearchResult searchResult, int page = 1)
        {
            int pageSize = 1; // количество элементов на странице
            var count = searchResult.TotalCount;
            var items = searchResult.Products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            searchResult.Products = items;
            PageInfo pageInfo = new PageInfo(count, page, pageSize);
            return pageInfo;
        }
        public ActionResult Select_car()
        {
            return View();
        }
    }
}