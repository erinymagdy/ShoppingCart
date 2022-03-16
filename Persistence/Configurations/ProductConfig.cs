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
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(c=>c.Id);
            builder.Property(p => p.Id).HasMaxLength(500).ValueGeneratedOnAdd();
            builder.Property(p => p.Description).HasMaxLength(2000);
            builder.Property(p => p.NameAr).HasMaxLength(150).IsRequired();
            builder.Property(p => p.NameEn).HasMaxLength(150).IsRequired();
            builder.Property(p => p.ProductImgPath).HasMaxLength(250);
            builder.HasOne(e => e.Category).WithMany(c => c.Products).HasForeignKey(c => c.CategoryId).IsRequired();
        }
    }
}

