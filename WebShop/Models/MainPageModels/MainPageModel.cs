using System.Collections.Generic;

namespace WebShop.Models.MainPageModels
{
    public class MainPageModel
    {
        public List<NewsViewModel> News { get; set; }
        public List<ProductCardViewModel> SearchResults { get; set; }
    }
}