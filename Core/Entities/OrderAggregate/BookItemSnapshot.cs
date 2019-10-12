using BookMania.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Core.Entities.OrderAggregate
{
    /// <summary>
    /// Represents a snapshot of the book when an order was made
    /// </summary>
    public class BookItemSnapshot // ValueObject
    {
        private BookItemSnapshot()
        {
        }

        // https://docs.microsoft.com/en-us/ef/core/modeling/constructors
        public BookItemSnapshot(int bookId, string title, string imageUrl = default)
        {
            title.ThrowIfNullOrWhiteSpace($"{nameof(title)} is invalid");

            BookId = bookId;
            ImageUrl = imageUrl;
            Title = title;
        }


        public int BookId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
