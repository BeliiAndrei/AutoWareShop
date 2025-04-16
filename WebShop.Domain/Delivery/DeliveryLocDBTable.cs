using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Domain.User.Auth;

namespace WebShop.Domain.User.Delivery
{
    public class DeliveryLocDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [StringLength(200)]
        public string Street { get; set; }

        public string House { get; set; }
        public string Block { get; set; }
        public string Apartment { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

    }
}