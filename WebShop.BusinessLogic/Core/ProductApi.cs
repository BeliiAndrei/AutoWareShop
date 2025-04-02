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
            using (var db = new ProductContext())
            {
                return db.Products.ToList();
            }
        }

        internal ProductDBTable GetProductByArticleAction(string article)
        {
            using (var db = new ProductContext())
            {
                var product = db.Products.FirstOrDefault(p=>p.Article == article);
                if (product == null)
                {
                    return null;
                }
                return product;
            }
        }

        internal ProductDBTable GetProductByIdAction(int id)
        {
            using (var db = new ProductContext())
            {
                var product = db.Products.Find(id);
                if (product == null)
                {
                    return null;
                }
                return product;
            }
        }
        internal List<ProductDBTable> GetProductsByStatus(ProductStatus status)
        {
            using (var db = new ProductContext())
            {
                return db.Products.Where(p => p.Status == status).ToList();
            }
        }

        internal ProductActionResponse CreateNewProductAction(ProductDTO data)
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
                    Quantity = data.Quantity
                };

                db.Products.Add(product);
                db.SaveChanges(); // Сохранение изменений в БД

                // Проверка: Поиск продукта в БД
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
                        StatusMsg = "Something went wrong. Product was not found after creation attempt"
                    };
                }
            }
        }

        internal void EditProduct(ProductDBTable product)
        {
            using (var db = new ProductContext())
            {
                db.Products.AddOrUpdate(product);
                db.SaveChanges();
            }
        }
    }
}
