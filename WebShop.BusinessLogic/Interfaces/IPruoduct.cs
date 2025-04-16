using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Product;
using WebShop.Domain.Product.Admin;
using WebShop.Domain.Product.SearchResponses;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        ProductActionResponse CreateNewProduct (ProductDTO data);
        ProductDTO ModifyProduct (ProductDTO data);
        ProductSearchResponseDTO GetProductsList(int page, int pageSize);
        ProductDTO GetProductById (int id);
        ProductDTO GetProductByArticle(string art);
        ProductSearchResponseDTO GetProductsByCategory(string category, int page = 0, int pageSize = 100);
        ProductSearchResponseDTO GetProductsBySearchString(string search_string, int page = 0, int pageSize = 100);
        ProductSearchResponseDTO GetProductsByStatus(string statusString, int page = 0, int pageSize = 100);

    }
}
