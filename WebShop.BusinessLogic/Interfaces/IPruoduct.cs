using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Product;
using WebShop.Domain.Product.Admin;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        ProductActionResponse CreateNewProduct (ProductDTO data);
        ProductDTO ModifyProduct (ProductDTO data);

        List<ProductDTO> GetProductsList();
        ProductDTO GetProductById (int id);
        ProductDTO GetProductByArticle(string art);
        List<ProductDTO> GetProductsByCategory(string category);
        List<ProductDTO> GetProductsBySearchString(string search_string);

    }
}
