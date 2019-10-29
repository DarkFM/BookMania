using System;
using System.Collections.Generic;

namespace BookMania.Data.Models
{
    public class Category
    {
        private Category()
        {
        }

        public Category(string name)
        {
            Name = name;
        }

        public int Id { get; private set; }
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
