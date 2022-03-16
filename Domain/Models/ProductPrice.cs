using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductPrice
    {
        public string ProductId { get; set; }
        public string SellerId { get; set; }
        public double SellerPrice { get; set; }
        public virtual Product Product { get; set; }
        public virtual ApplicationUser Seller { get; set; }

    }
}
