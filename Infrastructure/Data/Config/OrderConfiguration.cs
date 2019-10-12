using BookMania.Core.Entities.OrderAggregate;
using BookMania.Core.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMania.Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            // adds a shadow property to the application user on the order entity
            builder.HasOne(o => o.User)
                .WithMany(user => user.Orders)
                .OnDelete(DeleteBehavior.SetNull);

            // https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities#storing-owned-types-in-separate-tables
            builder.OwnsOne(o => o.ShipToAddress, b =>
            {
                b.ToTable("ShipToAddress");

                b.Property(e => e.Country).HasMaxLength(100).IsRequired();

                b.Property(e => e.CountryProvince).HasMaxLength(100).IsRequired();

                b.Property(e => e.ZipOrPostCode).HasMaxLength(15).IsRequired();

                b.Property(e => e.AddressLine1).HasMaxLength(255).IsRequired();

                b.Property(e => e.AddressLine2).HasMaxLength(255);

                b.Property(e => e.AddressLine3).HasMaxLength(255);
            });

            var orderItemsNavigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));
            orderItemsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsMany(o => o.OrderItems)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(o => o.TotalPrice);
        }
    }
}
