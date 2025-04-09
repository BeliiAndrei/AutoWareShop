using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.Enumerables;
using WebShop.Domain.Product;
using WebShop.Domain.Product.Admin;
using WebShop.Domain.User.Admin;

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

        public List<ProductDTO> GetProductsByCategory(string category)
        {
            var productsFromDB = GetProductsByCategoryAction(category);
            return FormList(productsFromDB);
        }
        public List<ProductDTO> FormList(List<ProductDBTable> productsFromDB)
        {
            List<ProductDTO> products = new List<ProductDTO>();
            foreach (var p in productsFromDB)
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
                products.Add(product);
            }
            return products;
        }
        public List<ProductDTO> GetProductsBySearchString(string search_string)
        {
            var productsFromDB = GetProductsBySearchStringAction(search_string);
            return FormList(productsFromDB);
        }

        public List<ProductDTO> GetProductsList()
        {
            return FormList(GetAllProducts());
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

        public List<ProductDTO> GetProductsByStatus(string statusString)
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
            return FormList(GetProductsByStatusAction(status));
        }
    }
}
