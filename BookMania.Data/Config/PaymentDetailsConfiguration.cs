using BookMania.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMania.Infrastructure.Data.Config
{
    public class PaymentDetailsConfiguration : IEntityTypeConfiguration<PaymentDetails>
    {
        public void Configure(EntityTypeBuilder<PaymentDetails> builder)
        {
            // shadow primary key
            builder.Property<int>("Id")
                .ValueGeneratedOnAdd();
            builder.HasKey("Id");

            builder.HasOne(p => p.User)
                .WithMany(user => user.PaymentOptions)
                .HasForeignKey(p => p.UserId);

            builder.OwnsOne(p => p.BillingAddress, b => 
            {
                b.ToTable("BillingAddress");

                b.Property(e => e.Country).HasMaxLength(100).IsRequired();

                b.Property(e => e.CountryProvince).HasMaxLength(100).IsRequired();

                b.Property(e => e.ZipOrPostCode).HasMaxLength(15).IsRequired();

                b.Property(e => e.AddressLine1).HasMaxLength(255).IsRequired();

                b.Property(e => e.AddressLine2).HasMaxLength(255);

                b.Property(e => e.AddressLine3).HasMaxLength(255);
            });

            builder.Property(p => p.NameOnCard)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.CardNumberHash)
                .IsRequired();
            
            builder.Property(p => p.CardSecurityKeyHash)
                .IsRequired();

            builder.Property(p => p.Salt)
                .IsRequired();
            
            builder.Property(p => p.Last4)
                .IsRequired();

            builder.Property(p => p.IsDefault)
                .HasDefaultValue(false);
        }
    }
}
