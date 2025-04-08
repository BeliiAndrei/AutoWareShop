using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.News.Image

{
    public class NewsImage
    {
        [Key]
        public int Id { get; set; }

        public byte[] ImageData { get; set; }
        public string ContentType { get; set; } // тип файла (image/png, image/jpeg и т.д.)

        public int NewsId { get; set; }
        [ForeignKey("NewsId")]
        public NewsDBTable News { get; set; }
    }
}
