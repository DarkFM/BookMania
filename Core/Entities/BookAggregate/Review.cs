using BookMania.Core.Entities.UserAggregate;
using BookMania.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Core.Entities.BookAggregate
{
    public class Review
    {
        private Review()
        {
        }

        public Review(int bookId, int userId, int? rating = null, string review = default)
        {
            if (rating > 5 || rating < 0)
                throw new ArgumentOutOfRangeException("The rating must be in the range [0, 5]");

            BookId = bookId;
            UserId = userId;
            Rating = rating;
            ReviewText = review;
        }

        public int BookId { get; private set; }
        public Book Book { get; set; }
        public int UserId { get; private set; }
        public ApplicationUser User { get; private set; }

        public int? Rating { get; set; }
        public string ReviewText { get; set; }
    }
}
