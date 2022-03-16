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
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id).HasMaxLength(500).ValueGeneratedOnAdd();
            builder.Property(p => p.NameAr).HasMaxLength(150).IsRequired();
            builder.Property(p => p.NameEn).HasMaxLength(150).IsRequired();
        }
    }
}
