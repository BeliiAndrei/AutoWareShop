﻿
using System.ComponentModel.DataAnnotations;


namespace WebShop.Models
{
    public class NewsViewModel
    {
        [Required(ErrorMessage = "Укажите заголовк")]
        public string Title { get; set; }

        public string Content { get; set; }
        [Required(ErrorMessage = "Укажите автора")]
        public string Author { get; set; }

        public string Category { get; set; }

        public string Tags { get; set; }

        public string ImageString { get; set; }


    }
}