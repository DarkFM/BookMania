using BookMania.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BookMania.Infrastructure.Data.Config
{
    public class BookItemSnapshotConfiguration : IEntityTypeConfiguration<BookItemSnapshot>
    {
        public void Configure(EntityTypeBuilder<BookItemSnapshot> builder)
        {
            builder.Property(e => e.Title)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(1000);
        }
    }
}
