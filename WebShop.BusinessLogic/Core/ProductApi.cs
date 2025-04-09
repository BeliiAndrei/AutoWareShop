using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using WebShop.BusinessLogic.DBModel.Seed;
using WebShop.Domain.Enumerables;
using WebShop.Domain.Product;
using WebShop.Domain.Product.Admin;

namespace WebShop.BusinessLogic.Core
{
    public class ProductApi
    {
        public ProductApi() { }

        internal List<ProductDBTable> GetAllProducts()
        {
            try
            {
                using (var db = new ProductContext())
                {
                    return db.Products.ToList();
                }
            }
            catch (Exception)
            {
                throw;  
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

        internal List<ProductDBTable> GetProductsByStatusAction(ProductStatus status)
        {
            try
            {
                using (var db = new ProductContext())
                {
                    return db.Products.Where(p => p.Status == status).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal List<ProductDBTable> GetProductsByCategoryAction(string category)
        {
            try
            {
                using (var db = new ProductContext())
                {
                    return db.Products
                             .Where(p => p.Category == category && p.Status != ProductStatus.hidden)
                             .ToList();
                }
            }
            catch (Exception)
            {
                return new List<ProductDBTable>();
            }
        }

        internal List<ProductDBTable> GetProductsBySearchStringAction(string searchString)
        {
            try
            {
                using (var db = new ProductContext())
                {
                    return db.Products
                             .Where(p => (p.Name.Contains(searchString) || p.Producer.Contains(searchString))
                                         && p.Status != ProductStatus.hidden)
                             .ToList();
                }
            }
            catch (Exception)
            {
                return new List<ProductDBTable>();
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
