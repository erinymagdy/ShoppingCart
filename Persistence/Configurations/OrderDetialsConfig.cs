using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class OrderDetialsConfig : IEntityTypeConfiguration<OrderDetials>
    {
        public void Configure(EntityTypeBuilder<OrderDetials> builder)
        {
            builder.ToTable("OrderDetials");
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id).HasMaxLength(500).ValueGeneratedOnAdd();
            builder.Property(p => p.Price).HasDefaultValue(0).IsRequired();
            builder.Property(p => p.Quantity).HasDefaultValue(0).IsRequired();
            builder.HasOne(e => e.Order).WithMany(c => c.OrderDetials).HasForeignKey(c => c.OrderId).IsRequired();
            builder.HasOne(e => e.Seller).WithMany(c => c.OrderDetials).HasForeignKey(c => c.SellerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e => e.Product).WithMany(c => c.OrderDetials).HasForeignKey(c => c.ProductId).IsRequired();
        }
    }
}
