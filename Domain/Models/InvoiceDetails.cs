using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class InvoiceDetails : BaseEntity
    {
        public InvoiceDetails()
        {
            Quantity = 0;
            Price = 0;
        }
        public string ProductId { get; set; }
        public string InvoiceId { get; set; }
        public string SellerId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual ApplicationUser Seller { get; set; }
    }
}

