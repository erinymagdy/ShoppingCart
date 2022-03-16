using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderDetials = new HashSet<OrderDetials>();
        }
        public Guid OrderNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string CustomerId { get; set; }
        public double TotalPrice { get; set; }

        public virtual ApplicationUser Customer { get; set; }
        public virtual Invoice Invoice  { get; set; }
        public ICollection<OrderDetials> OrderDetials { get; set; }


    }
}
