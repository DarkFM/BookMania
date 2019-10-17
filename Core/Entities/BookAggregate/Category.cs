using BookMania.Core.Extensions;
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
    }
}
