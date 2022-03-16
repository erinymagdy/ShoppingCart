using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence
{
    public class RolesConfig : IEntityTypeConfiguration<IdentityRole<string>>
    {

        public void Configure(EntityTypeBuilder<IdentityRole<string>> builder)
        {
            builder.Property(p => p.Id).HasMaxLength(500);
            builder.Property(p => p.NormalizedName).HasMaxLength(500);
            builder.Property(p => p.Name).HasMaxLength(500);
            var roles = new IdentityRole<string>[]
            {
                new IdentityRole<string>
                {
                    Id="1",
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole<string>
                {
                    Id="2",
                    Name="Seller",
                    NormalizedName="Seller".ToUpper()
                },
                new IdentityRole<string>
                {
                    Id="3",
                    Name="Customer",
                    NormalizedName="Customer".ToUpper()
                },
            };
            builder.HasData(roles);
        }
    }
}
