using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{

    public class Product:BaseEntity 
    {
        public Product()
        {
            ProductPrices = new HashSet<ProductPrice>();
            OrderDetials = new HashSet<OrderDetials>();
            InvoiceDetails = new HashSet<InvoiceDetails>();
        }
        [Display(Name = "ProductName")]
        public string NameEn { get; set; }
        [Display(Name = "ProductName")]
        public string NameAr { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        [Display (Name ="Image")]
        public string ProductImgPath { get; set; }
        public bool InStock { get; set; }
        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        [Display(Name = "Category")]
        public Category Category{ get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
        public virtual ICollection<OrderDetials> OrderDetials { get; set; }
        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }

    }

}
