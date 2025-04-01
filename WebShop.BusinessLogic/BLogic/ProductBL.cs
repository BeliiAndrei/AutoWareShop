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
            return product;
        }

        public List<ProductDTO> GetProductsList()
        {
            var productsFromDB = GetAllProducts();
            var productsList = new List<ProductDTO>();
            foreach (var p in productsFromDB)
            {
                ProductDTO m = new ProductDTO();
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
                productsList.Add(m);
            }
            return productsList;
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
            EditProduct(product);
            return GetProductById(product.Id);
        }
    }
}
