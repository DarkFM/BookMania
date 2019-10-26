using BookMania.Core.Extensions;
using System;
using System.Collections.Generic;

namespace BookMania.Core.Entities.BookAggregate
{
    public class Category : BaseEntity
    {
        private Category()
        {
        }

        public Category(string name)
        {
            name.ThrowIfNullOrWhiteSpace($"{nameof(name)} cannot be null/blank");

            Name = name;
        }

        public string Name { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }

        public override bool Equals(object x)
        {
            var newX = x as Category;
            return (newX.Name == this.Name) && (newX.Id == this.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Id);
        }
    }
}
