using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Invoice : BaseEntity
    {
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetails>();
        }
    
        public Guid InvoiceNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public string OrderId { get; set; }
        public double TotalInvoice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public virtual Order Order { get; set; }
        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
