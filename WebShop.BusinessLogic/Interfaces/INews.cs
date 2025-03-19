using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.Core;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface INews
    {
        void CreateNews(News news);
        void UpdateNews(News news);
        void DeleteNews(int newsId);
    }
}
