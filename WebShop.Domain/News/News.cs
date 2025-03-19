using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.News
{
    public class News
    {
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Images { get; set; } = new List<string>();
    }
}
