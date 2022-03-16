
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<ProductPrice> ProductPrices { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderDetials> OrderDetials { get; set; }
            public DbSet<Invoice> Invoices { get; set; }
            public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
     

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserRole<string>>(e =>
            {
                e.HasKey(l => new { l.UserId, l.RoleId });
            });
            builder.Entity<IdentityUserToken<string>>(e =>
            {
                e.HasKey(l => new { l.UserId, l.Value });
                e.Property(p => p.LoginProvider).HasMaxLength(500);
                e.Property(p => p.Name).HasMaxLength(500);
            });
            builder.Entity<IdentityUserLogin<string>>(e =>
            {
                e.HasKey(l => new { l.LoginProvider, l.ProviderKey });
                e.Property(p => p.LoginProvider).HasMaxLength(500);
                e.Property(p => p.ProviderKey).HasMaxLength(500);
                e.Property(p => p.ProviderDisplayName).HasMaxLength(500);
            });
            //builder.ApplyConfiguration(new RolesConfig());
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        }
        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }

}
