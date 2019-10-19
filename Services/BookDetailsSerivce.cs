using BookMania.Core.Interfaces;
using BookMania.Interfaces;
using BookMania.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Services
{
    public class BookDetailsSerivce : IBookDetailsService
    {
        private readonly IBookRepository _bookRepository;
        public BookDetailsSerivce(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookDetailsViewModel> GetBookDetailsAsync(int bookId, int userId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            var vm = new BookDetailsViewModel
            {
                Authors = book.BookAuthors.Select(b => b.Author.Name),
                BookId = book.Id,
                Description = book.Description,
                ImageUrl = book.ImageUrlLarge,
                IsFavorite = book.Favorites.Any(f => f.UserId == userId),
                Price = book.Price,
                PublishedDate = book.PublishedDate,
                Publisher = book.Publisher.Name,
                Title = book.Title,
                Reviews = book.Reviews.Select(r => new ReviewViewModel() { Rating = r.Rating, ReviewText = r.ReviewText, UserName = r.User.UserName }),
                AverageRating = (decimal?)book.Reviews.Average(r => r.Rating)
            };

            return vm;
        }
    }
}
