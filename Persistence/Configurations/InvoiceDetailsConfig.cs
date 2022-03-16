using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Persistence.Configurations
{
    public class InvoiceDetailsConfig : IEntityTypeConfiguration<InvoiceDetails>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetails> builder)
        {
            builder.ToTable("InvoiceDetails");
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id).HasMaxLength(500).ValueGeneratedOnAdd();
            builder.Property(p => p.Price).HasDefaultValue(0).IsRequired();
            builder.Property(p => p.Quantity).HasDefaultValue(0).IsRequired();
            builder.HasOne(e => e.Invoice).WithMany(c => c.InvoiceDetails).HasForeignKey(c => c.InvoiceId).IsRequired();
            builder.HasOne(e => e.Seller).WithMany(c => c.InvoiceDetails).HasForeignKey(c => c.SellerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e => e.Product).WithMany(c => c.InvoiceDetails).HasForeignKey(c => c.ProductId).IsRequired();
        }
    }
}
