using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Product.SearchResponses
{
    public class ProductSearchResponseDTO
    {
        public List<ProductDTO> products { get; set; }
        public int productsTotalCount { get; set; }

        public List<string> brands { get; set; }
    }
}
