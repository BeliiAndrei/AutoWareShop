using System;
using System.Collections.Generic;

namespace WebShop.Domain.News
{
    public class News
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Images { get; set; }
        public string Category { get; set; }  
        public string Tags { get; set; }
        public DateTime CreationDate { get; set; }
        public int Id { get; set; }
    }
}
