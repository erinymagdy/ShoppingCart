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
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id).HasMaxLength(500).ValueGeneratedOnAdd();
            builder.Property(p => p.InvoiceNumber).HasMaxLength(500).ValueGeneratedOnAdd();
            builder.Property(p => p.CreationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(p => p.PaymentMethod).IsRequired();
            builder.Property(p => p.TotalInvoice).HasDefaultValue(0).IsRequired();
            builder.HasOne(e => e.Order).WithOne(c => c.Invoice).IsRequired();
        }
    }
}
