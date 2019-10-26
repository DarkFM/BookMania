using BookMania.Core.Extensions;
using System;
using System.Collections.Generic;

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

        public override bool Equals(object x)
        {
            var newX = x as Author;
            return (newX.Name == this.Name) && (newX.Id == this.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Id);
        }
    }
}
