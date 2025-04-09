
using System.ComponentModel.DataAnnotations;
using WebShop.Domain.News;


namespace WebShop.Models
{
    public class NewsViewModel
    {
        [Required(ErrorMessage = "Укажите заголовк")]
        public string Title { get; set; }

        public int Id { get; set; }

        public string Content { get; set; }
        [Required(ErrorMessage = "Укажите автора")]
        public string Author { get; set; }

        public string Category { get; set; }

        public string Tags { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }



    }
  
    }