using System.Collections.Generic;

namespace BookMania.Data.Models
{
    public class Publisher
    {
        private Publisher()
        {
        }

        public Publisher(string name)
            :this(name, new List<Book>())
        {
        }

        public Publisher(string name, List<Book> books)
        {
            Name = name;
            Books = books;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }

        public override bool Equals(object x)
        {
            var newX = x as Publisher;
            return newX.Name == this.Name;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Id);
        }
    }
}
