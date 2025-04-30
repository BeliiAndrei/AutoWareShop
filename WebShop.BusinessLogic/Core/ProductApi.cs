using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using WebShop.BusinessLogic.DBModel.Seed;
using WebShop.Domain.Enumerables;
using WebShop.Domain.Product;
using WebShop.Domain.Product.Admin;
using WebShop.Domain.Product.SearchResponses;

namespace WebShop.BusinessLogic.Core
{
    public class ProductApi
    {
        public ProductApi() { }

        internal ProductSearchResponseDB GetAllProducts(int page, int pageSize)
        {
            try
            {
                using (var db = new ProductContext())
                {
                    var pr = db.Products.OrderBy(p => p.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    var count = db.Products.Count();
                    var response = new ProductSearchResponseDB
                    {
                        products = pr,
                        productsTotalCount = count
                    };
                    return response;
                }
            }
            catch (Exception)
            {
                return new ProductSearchResponseDB
                {
                    products = null,
                    productsTotalCount = 0
                };
            }
        }

        internal ProductSearchResponseDB GetAllProducts(int page, int pageSize,
            decimal minPrice, decimal maxPrice, bool onlyAvailable, List<string> brands)
        {
            try
            {
                using (var db = new ProductContext())
                {
                    var query = db.Products.AsQueryable();

                    // Фильтрация по цене
                    query = query.Where(p => p.Price >= minPrice && p.Price <= maxPrice);

                    // Фильтрация по наличию
                    if (onlyAvailable)
                    {
                        query = query.Where(p => p.Quantity > 0);
                    }

                    // Фильтрация по брендам
                    if (brands != null && brands.Any())
                    {
                        query = query.Where(p => brands.Contains(p.Producer));
                    }

                    var count = query.Count();

                    var products = query.OrderBy(p => p.Id)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                    // Список брендов для отображения в фильтре
                    var allBrands = query.Select(p => p.Producer)
                                         .Distinct()
                                         .OrderBy(name => name)
                                         .ToList();

                    return new ProductSearchResponseDB
                    {
                        products = products,
                        productsTotalCount = count,
                        availableBrands = allBrands
                    };
                }
            }
            catch (Exception)
            {
                return new ProductSearchResponseDB
                {
                    products = null,
                    productsTotalCount = 0,
                    availableBrands = new List<string>()
                };
            }
        }

        internal ProductDBTable GetProductByArticleAction(string article)
        {
            try
            {
                using (var db = new ProductContext())
                {
                    return db.Products.FirstOrDefault(p => p.Article == article);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal ProductDBTable GetProductByIdAction(int id)
        {
            try
            {
                using (var db = new ProductContext())
                {
                    return db.Products.Find(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal ProductSearchResponseDB GetProductsByStatusAction(ProductStatus status, int page, int pageSize,
            decimal minPrice, decimal maxPrice, bool onlyAvailable, List<string> brands)
        {
            try
            {
                using (var db = new ProductContext())
                {
                    var query = db.Products.AsQueryable();

                    // Фильтрация по статусу
                    query = query.Where(p => p.Status == status);

                    // Фильтрация по цене
                    query = query.Where(p => p.Price >= minPrice && p.Price <= maxPrice);

                    // Фильтрация по наличию
                    if (onlyAvailable)
                    {
                        query = query.Where(p => p.Quantity > 0);
                    }

                    // Фильтрация по брендам
                    if (brands != null && brands.Any())
                    {
                        query = query.Where(p => brands.Contains(p.Producer));
                    }

                    var count = query.Count();

                    var products = query.OrderBy(p => p.Id)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                    // Список брендов для отображения в фильтре
                    var allBrands = query.Select(p => p.Producer)
                                         .Distinct()
                                         .OrderBy(name => name)
                                         .ToList();

                    return new ProductSearchResponseDB
                    {
                        products = products,
                        productsTotalCount = count,
                        availableBrands = allBrands
                    };
                }
            }
            catch (Exception)
            {
                return new ProductSearchResponseDB
                {
                    products = null,
                    productsTotalCount = 0,
                    availableBrands = new List<string>()
                };
            }
        }


        internal ProductSearchResponseDB GetProductsByCategoryAction(string category, int page, int pageSize,
            decimal minPrice, decimal maxPrice, bool onlyAvailable, List<string> brands)
        {
            try
            {
                using (var db = new ProductContext())
                {
                    var query = db.Products.AsQueryable();

                    // Фильтрация по категории
                    if (!string.IsNullOrEmpty(category))
                        query = query.Where(p => p.Category == category);

                    // Фильтрация: только видимые товары
                    query = query.Where(p => p.Status != ProductStatus.hidden);

                    // Фильтрация по цене
                    query = query.Where(p => p.Price >= minPrice && p.Price <= maxPrice);

                    // Фильтрация по наличию
                    if (onlyAvailable)
                        query = query.Where(p => p.Quantity > 0);

                    // Фильтрация по брендам
                    if (brands != null && brands.Any())
                        query = query.Where(p => brands.Contains(p.Producer));

                    var count = query.Count();

                    var products = query.OrderBy(p => p.Id)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                    // Список всех брендов для фильтра
                    var allBrands = db.Products
                                      .Where(p => p.Category == category && p.Status != ProductStatus.hidden)
                                      .Select(p => p.Producer)
                                      .Distinct()
                                      .OrderBy(name => name)
                                      .ToList();

                    return new ProductSearchResponseDB
                    {
                        products = products,
                        productsTotalCount = count,
                        availableBrands = allBrands 
                    };
                }
            }
            catch (Exception)
            {
                return new ProductSearchResponseDB
                {
                    products = null,
                    productsTotalCount = 0,
                    availableBrands = new List<string>()
                };
            }
        }

        internal ProductSearchResponseDB GetProductsBySearchStringAction(string searchString, int page, int pageSize,
            decimal minPrice, decimal maxPrice, bool onlyAvailable, List<string> brands)
        {
            try
            {
                using (var db = new ProductContext())
                {
                    var query = db.Products.AsQueryable();

                    // Поиск по названию или производителю
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        query = query.Where(p => (p.Name.Contains(searchString) || p.Producer.Contains(searchString)));
                    }

                    // Только видимые товары
                    query = query.Where(p => p.Status != ProductStatus.hidden);

                    // Фильтрация по цене
                    query = query.Where(p => p.Price >= minPrice && p.Price <= maxPrice);

                    // Фильтрация по наличию
                    if (onlyAvailable)
                    {
                        query = query.Where(p => p.Quantity > 0);
                    }

                    // Фильтрация по брендам
                    if (brands != null && brands.Any())
                    {
                        query = query.Where(p => brands.Contains(p.Producer));
                    }

                    var count = query.Count();

                    var products = query.OrderBy(p => p.Id)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                    // Список всех брендов для отображения в фильтрах
                    var allBrands = query.Select(p => p.Producer)
                                         .Distinct()
                                         .OrderBy(name => name)
                                         .ToList();

                    return new ProductSearchResponseDB
                    {
                        products = products,
                        productsTotalCount = count,
                        availableBrands = allBrands // новое свойство!
                    };
                }
            }
            catch (Exception)
            {
                return new ProductSearchResponseDB
                {
                    products = null,
                    productsTotalCount = 0,
                    availableBrands = new List<string>()
                };
            }
        }
        internal ProductActionResponse CreateNewProductAction(ProductDTO data)
        {
            try
            {
                using (var db = new ProductContext())
                {
                    var isSuchArticle = db.Products.FirstOrDefault(p => p.Article == data.Article);
                    if (isSuchArticle != null)
                    {
                        return new ProductActionResponse
                        {
                            Status = false,
                            StatusMsg = "Product with such Article already exists"
                        };
                    }

                    var product = new ProductDBTable
                    {
                        Name = data.Name,
                        Producer = data.Producer,
                        Description = data.Description,
                        Category = data.Category,
                        Price = data.Price,
                        Status = data.Status,
                        Article = data.Article,
                        Quantity = data.Quantity,
                        ImageString = data.ImageNumber
                    };

                    db.Products.Add(product);
                    db.SaveChanges();

                    var savedProduct = db.Products.FirstOrDefault(p => p.Id == product.Id);
                    if (savedProduct != null)
                    {
                        return new ProductActionResponse
                        {
                            Status = true,
                            StatusMsg = "Product added successfully"
                        };
                    }
                    else
                    {
                        return new ProductActionResponse
                        {
                            Status = false,
                            StatusMsg = "Product was not found after creation attempt"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ProductActionResponse
                {
                    Status = false,
                    StatusMsg = $"Error: {ex.Message}"
                };
            }
        }

        internal void EditProduct(ProductDBTable product)
        {
            try
            {
                using (var db = new ProductContext())
                {
                    db.Products.AddOrUpdate(product);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
