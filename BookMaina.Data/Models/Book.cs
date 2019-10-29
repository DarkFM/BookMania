using System;
using System.Collections.Generic;

namespace BookMania.Data.Models
{
    public class Book
    {
        private Book()
        {
        }

        public Book(string title, decimal price, DateTime publishedDate, Publisher publisher, string description = null, string imageUrl = null, string imageUrlLarge = null)
        {
            Title = title;
            Price = price;
            PublishedDate = publishedDate;
            Publisher = publisher;
            Description = description;
            ImageUrl = imageUrl;
            ImageUrlLarge = imageUrlLarge;
        }


        public int Id { get; private set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlLarge { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
