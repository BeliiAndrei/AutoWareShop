using System.Collections.Generic;
using WebShop.Domain.News;
using WebShop.Models;

namespace WebShop.Models.MainPageModels
{
    public class MainPageModel
    {
        public List<ProductCardViewModel> SearchResults { get; set; }
        public List<News> News { get; set; } // Просто список новостей, без лишних условий
    }
}