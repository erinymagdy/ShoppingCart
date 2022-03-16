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
    public class ProductPricesConfig : IEntityTypeConfiguration<ProductPrice>
    {
        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.ToTable("ProductPrices");
            builder.HasKey(c => new { c.ProductId, c.SellerId });
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.SellerId).IsRequired();
            builder.Property(p => p.SellerPrice).HasDefaultValue(0).IsRequired();
            builder.HasOne(e => e.Seller).WithMany(c => c.ProductPrices).HasForeignKey(c => c.SellerId).IsRequired();
            builder.HasOne(e => e.Product).WithMany(c => c.ProductPrices).HasForeignKey(c => c.ProductId).IsRequired();
        }
    
    }
}
