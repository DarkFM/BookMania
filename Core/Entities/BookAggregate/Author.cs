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

        public Author(string name)
        {
            name.ThrowIfNullOrWhiteSpace($"{nameof(name)} cannot be null or whitespace");

            Name = name;
        }

        public string Name { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
