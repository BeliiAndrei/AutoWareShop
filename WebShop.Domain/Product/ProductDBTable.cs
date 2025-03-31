using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Product
{
    public class ProductDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username cannot be longer than 30 characters.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Producer")]
        public string Producer { get; set; }

        [Display(Name = "Images")]
        public List<string> ImagesLinks { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Article")]
        public string Article { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

    }
}
