using BookMania.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMania.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            //// https://stackoverflow.com/a/51504915/7771568
            //// shadow primary key
            //builder.Property<int>("Id")
            //    .HasColumnType("int")
            //    .ValueGeneratedOnAdd()
            //    .HasAnnotation("Key", 0);

            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
