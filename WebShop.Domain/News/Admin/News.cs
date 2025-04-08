using System;
using System.Collections.Generic;

namespace WebShop.Domain.News
{
    public class News
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Tags { get; set; }
        public List<NewsImageDto> Images { get; set; }
    }

    public class NewsImageDto
    {
        public int Id { get; set; }
        public string ContentType { get; set; }
        public string Base64Data { get; set; }
    }
}
