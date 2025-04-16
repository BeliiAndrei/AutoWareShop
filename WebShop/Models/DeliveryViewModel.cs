using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class DeliveryViewModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "укажите код почты")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "укажите город")]
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Block { get; set; }
        public string Apartment { get; set; }
        public string Comment { get; set; }
    }
}
