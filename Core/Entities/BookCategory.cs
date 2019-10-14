using BookMania.Core.Entities.BookAggregate;

namespace BookMania.Core.Entities
{
    public class BookCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
