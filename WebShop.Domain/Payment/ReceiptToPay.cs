using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Product
{
    public class ReceiptToPay
    {
        public string Id { get; set; }
        public decimal TotalPrice { get; set; }

        public List<ProductDTO> Products { get; set; }
    }
}
