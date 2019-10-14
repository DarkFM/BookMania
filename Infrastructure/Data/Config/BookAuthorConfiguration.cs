using BookMania.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMania.Infrastructure.Data.Config
{
    public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasKey(bo => new { bo.AuthorId, bo.BookId });

            builder.HasOne(ba => ba.Book)
                .WithMany(book => book.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            builder.HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

        }
    }
}
