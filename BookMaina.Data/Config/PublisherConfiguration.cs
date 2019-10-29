using BookMania.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMania.Infrastructure.Data.Config
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Name).IsUnique();

            builder.Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
