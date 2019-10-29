using System;
using System.Collections.Generic;

namespace BookMania.Data.Models
{
    public class Author
    {
        private Author()
        {
        }
        
        public Author(string name)
        {
            Name = name;
        }

        public int Id { get; private set; }
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
