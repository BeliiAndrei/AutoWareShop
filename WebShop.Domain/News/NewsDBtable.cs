using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebShop.Domain.Enumerables;
using System;

namespace WebShop.Domain.News
{
    public class NewsDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Image")]
        public string ImageString { get; set; }

        [Display(Name = "Tags")]
        public string Tags { get; set; }
    }
}
