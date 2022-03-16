using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Persistence.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id).HasMaxLength(500).ValueGeneratedOnAdd();
            builder.Property(p => p.OrderNumber).HasMaxLength(500).ValueGeneratedOnAdd();
            builder.Property(p => p.CreationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(p => p.OrderStatus).IsRequired();
            builder.Property(p => p.TotalPrice).HasDefaultValue(0).IsRequired();
            builder.HasOne(e => e.Customer).WithMany(c => c.Orders).HasForeignKey(c => c.CustomerId).IsRequired().OnDelete(DeleteBehavior.NoAction);

        }
    }
}
