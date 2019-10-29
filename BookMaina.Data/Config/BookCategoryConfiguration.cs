using BookMania.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMania.Data.Config
{
    public class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(bc => new { bc.CategoryId, bc.BookId });

            builder.HasOne(bc => bc.Book)
                .WithMany(book => book.BookCategories)
                .HasForeignKey(ba => ba.BookId);

            builder.HasOne(ba => ba.Category)
                .WithMany(cat => cat.BookCategories)
                .HasForeignKey(ba => ba.CategoryId);
        }
    }
}
