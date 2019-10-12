using BookMania.Core.Entities.BookAggregate;
using BookMania.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Core.Entities
{
    public class Category // ValueObject
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
