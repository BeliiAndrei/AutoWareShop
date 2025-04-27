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
        ProductSearchResponseDTO GetProductsByCategory(string category, int page, int pageSize,
            decimal minPrice, decimal maxPrice, bool onlyAvailable, List<string> brands);
        ProductSearchResponseDTO GetProductsBySearchString(string searchString, int page, int pageSize,
            decimal minPrice, decimal maxPrice, bool onlyAvailable, List<string> brands);
        ProductSearchResponseDTO GetProductsByStatus(string statusString, int page, int pageSize, 
            decimal minPrice, decimal maxPrice, bool onlyAvailable, List<string> brands);

    }
}
