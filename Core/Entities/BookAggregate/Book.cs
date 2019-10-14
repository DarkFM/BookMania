using BookMania.Core.Entities.UserAggregate;
using BookMania.Core.Extensions;
using BookMania.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookMania.Core.Entities.BookAggregate
{
    public class Book: BaseEntity, IAggregateRoot
    {
        private Book()
        {
        }

        public Book(string title, decimal price, DateTime publishedDate, Publisher publisher, string description = null, string imageUrl = null)
        {
            title.ThrowIfNullOrWhiteSpace($"{nameof(title)} cannot be null/white-space");
            publisher.ThrowIfNull($"A {nameof(Book)} must have a {nameof(publisher)}");

            Title = title;
            Price = price;
            PublishedDate = publishedDate;
            Publisher = publisher;
            Description = description;
            ImageUrl = imageUrl;
        }

        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
