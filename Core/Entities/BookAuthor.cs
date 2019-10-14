using BookMania.Core.Entities.BookAggregate;

namespace BookMania.Core.Entities
{
    public class BookAuthor
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
