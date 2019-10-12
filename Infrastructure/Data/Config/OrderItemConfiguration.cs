using BookMania.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Infrastructure.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // https://stackoverflow.com/a/51504915/7771568
            // shadow primary key
            builder.Property<int>("Id")
                .ValueGeneratedOnAdd();
            builder.HasKey("Id");

            // https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities
            // making this entity owner of another entity (BookItemSnapshot).
            // the means the owned entity (BookItemSnapshot) can only ever be accessed from this Entity
            // Owned entities are essentially a part of the owner and cannot exist without it, they are conceptually similar to aggregates.
            builder.OwnsOne(oi => oi.BookOrdered);

            builder.Property(e => e.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.Quantity)
                .IsRequired();
        }
    }
}
