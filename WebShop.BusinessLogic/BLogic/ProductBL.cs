﻿using System;
using System.Collections.Generic;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Enumerables;
using WebShop.Domain.Product;
using WebShop.Domain.Product.Admin;
using WebShop.Domain.Product.SearchResponses;

namespace WebShop.BusinessLogic.BLogic
{
    public class ProductBL : ProductApi, IProduct
    {
        public ProductActionResponse CreateNewProduct(ProductDTO data)
        {
            return CreateNewProductAction(data);
        }

        public ProductDTO GetProductByArticle(string art)
        {
            var p = GetProductByArticleAction(art);
            ProductDTO m = new ProductDTO();
            if (p == null)
                return null;
            m.Id = p.Id;
            m.Name = p.Name;
            m.Producer = p.Producer;
            m.Article = p.Article;
            m.Price = p.Price;
            m.Category = p.Category;
            m.Status = p.Status;
            m.Quantity = p.Quantity;
            m.ImageNumber = p.ImageString;
            m.Description = p.Description;
            return m;
        }

        public ProductDTO GetProductById(int id)
        {
            var productFromDB = GetProductByIdAction(id);
            ProductDTO product = new ProductDTO();
            product.Id = productFromDB.Id;
            product.Name = productFromDB.Name;
            product.Producer = productFromDB.Producer;
            product.Description = productFromDB.Description;
            product.Category = productFromDB.Category;
            product.Price = productFromDB.Price;
            product.Quantity = productFromDB.Quantity;
            product.Article = productFromDB.Article;
            product.ImageNumber = productFromDB.ImageString;
            product.Status = productFromDB.Status;
            return product;
        }

        public ProductSearchResponseDTO GetProductsByCategory(string category, int page, int pageSize,
            decimal minPrice, decimal maxPrice, bool onlyAvailable, List<string> brands)
        {
            var productsFromDB = GetProductsByCategoryAction(category, page, pageSize, minPrice, maxPrice, onlyAvailable, brands);
            return FormList(productsFromDB);
        }
        internal ProductSearchResponseDTO FormList(ProductSearchResponseDB response)
        {
            List<ProductDTO> productsL = new List<ProductDTO>();
            foreach (var p in response.products)
            {
                var product = new ProductDTO
                {
                    Name = p.Name,
                    Producer = p.Producer,
                    Description = p.Description,
                    Category = p.Category,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Article = p.Article,
                    Id = p.Id,
                    ImageNumber = p.ImageString,
                    Status = p.Status
                };
                productsL.Add(product);
            }
            
            return new ProductSearchResponseDTO 
            { 
                products = productsL, productsTotalCount = response.productsTotalCount, brands = response.availableBrands
            };
        }
        public ProductSearchResponseDTO GetProductsBySearchString(string searchString, int page, int pageSize,
            decimal minPrice, decimal maxPrice, bool onlyAvailable, List<string> brands)
        {
            var productsFromDB = GetProductsBySearchStringAction(searchString, page, pageSize, minPrice, maxPrice, onlyAvailable, brands);
            return FormList(productsFromDB);
        }

        public ProductSearchResponseDTO GetProductsList(int page, int pageSize)
        {
            return FormList(GetAllProducts(page,pageSize));
        }

        public ProductSearchResponseDTO GetProductsList(int page, int pageSize,
            decimal minPrice, decimal maxPrice, bool onlyAvailable, List<string> brands)
        {
            var productsFromDB = GetAllProducts(page, pageSize, minPrice, maxPrice, onlyAvailable, brands);
            return FormList(productsFromDB);
        }

        public ProductDTO ModifyProduct(ProductDTO oldProduct)
        {
            var product = GetProductByArticleAction(oldProduct.Article);
            if (product == null)
                return null;
            product.Name = oldProduct.Name;
            product.Producer = oldProduct.Producer;
            product.Description = oldProduct.Description;
            product.Category = oldProduct.Category;
            product.Price = oldProduct.Price;
            product.Quantity = oldProduct.Quantity;
            product.Article = oldProduct.Article;
            product.ImageString = oldProduct.ImageNumber;
            product.Status = oldProduct.Status;
            EditProduct(product);
            return GetProductById(product.Id);
        }

        public ProductSearchResponseDTO GetProductsByStatus(string statusString, int page, int pageSize,
            decimal minPrice, decimal maxPrice, bool onlyAvailable, List<string> brands)
        {
            ProductStatus status;
            try
            {
                status = (ProductStatus)Enum.Parse(typeof(ProductStatus), statusString);
            }
            catch
            {
                return null;
            }
            return FormList(GetProductsByStatusAction(status, page, pageSize, minPrice, maxPrice, onlyAvailable, brands));
        }
    }
}