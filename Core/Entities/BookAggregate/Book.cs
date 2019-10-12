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

        public Book(string title, decimal price, DateTime publishedDate, int publisherId, List<BookAuthor> bookAuthors)
            : this(title, price, publishedDate, publisherId, bookAuthors, new List<Favorite>())
        {

        }

        public Book(string title, decimal price, DateTime publishedDate, int publisherId, List<BookAuthor> bookAuthors,
            List<Favorite> favorites, string description = null, string imageUrl = null)
        {
            title.ThrowIfNullOrWhiteSpace($"{nameof(title)} cannot be null/white-space");
            bookAuthors.ThrowIfNullOrEmpty($"{nameof(bookAuthors)} cannot be null/empty");
            
            Title = title;
            Price = price;
            PublishedDate = publishedDate;
            PublisherId = publisherId;
            BookAuthors = bookAuthors;
            Description = description;
            ImageUrl = imageUrl;
            Favorites = favorites;
        }

        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
