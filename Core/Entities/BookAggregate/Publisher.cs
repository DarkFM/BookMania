using BookMania.Core.Extensions;
using System.Collections.Generic;

namespace BookMania.Core.Entities.BookAggregate
{
    public class Publisher : BaseEntity
    {
        private Publisher()
        {
        }

        public Publisher(string name, List<Book> books)
        {
            name.ThrowIfNullOrWhiteSpace($"{nameof(name)} cannot be null/whitespace");

            Name = name;
            Books = books;
        }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
