using BookMania.Core.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMania.Infrastructure.Data.Config
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var paymentNavigation = builder.Metadata.FindNavigation(nameof(ApplicationUser.PaymentOptions));
            paymentNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            var favNavigation = builder.Metadata.FindNavigation(nameof(ApplicationUser.Favorites));
            favNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsOne(u => u.ShippingAddress);
        }
    }
}
