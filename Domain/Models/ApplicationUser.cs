
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Orders = new HashSet<Order>();
            OrderDetials = new HashSet<OrderDetials>();
            InvoiceDetails = new HashSet<InvoiceDetails>();
            ProductPrices = new HashSet<ProductPrice>();
        }
        public ICollection<Order> Orders { get; set; }
        public ICollection<OrderDetials> OrderDetials { get; set; }
        public ICollection<InvoiceDetails> InvoiceDetails { get; set; }
        public ICollection<ProductPrice> ProductPrices { get; set; }
    }
}
