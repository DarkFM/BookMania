using BookMania.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Core.Entities.BookAggregate
{
    public class Author : BaseEntity
    {
        private Author()
        {
        }

        public Author(string name, List<BookAuthor> bookAuthors)
        {
            name.ThrowIfNullOrWhiteSpace($"{nameof(name)} cannot be null or whitespace");
            bookAuthors.ThrowIfNullOrEmpty($"{nameof(bookAuthors)} cannot be null or whitespace");

            Name = name;
            BookAuthors = bookAuthors;
        }

        public string Name { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
