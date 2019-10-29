using BookMania.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMania.Data.Config
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var paymentNavigation = builder.Metadata.FindNavigation(nameof(ApplicationUser.PaymentOptions));
            paymentNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            var favNavigation = builder.Metadata.FindNavigation(nameof(ApplicationUser.Favorites));
            favNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsOne(u => u.ShippingAddress, b =>
            {
                b.ToTable("UserAddress");

                b.Property(e => e.Country).HasMaxLength(100);

                b.Property(e => e.CountryProvince).HasMaxLength(100);

                b.Property(e => e.ZipOrPostCode).HasMaxLength(15);

                b.Property(e => e.AddressLine1).HasMaxLength(255);

                b.Property(e => e.AddressLine2).HasMaxLength(255);

                b.Property(e => e.AddressLine3).HasMaxLength(255);
            });
        }
    }
}
