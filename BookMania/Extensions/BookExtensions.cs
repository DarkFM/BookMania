using BookMania.Data.Models;
using BookMania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Extensions
{
    public static class BookExtensions
    {
        public static BookModel ToBookModel(this Book book, bool isUserFavourite = false)
        {
            return new BookModel
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                ImageUrl = book.ImageUrl,
                Description = book.Description,
                PublishedDate = book.PublishedDate,
                Publisher = book.Publisher.ToPublisherModel(),
                Reviews = book.Reviews.Select(r => r.ToReviewModel()),
                Authors = book.BookAuthors.Select(ba => ba.Author.ToAuthorModel()),
                AverageRating = (decimal?)book.Reviews.Select(r => r.Rating).Average(),
                Categories = book.BookCategories.Select(bc => bc.Category.ToCategoryModel()),
                IsFavorite = isUserFavourite
            };
        }
    }
}
