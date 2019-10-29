using BookMania.Core;
using BookMania.Data;
using BookMania.Data.Interfaces;
using BookMania.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Service
{
    public class BookService : IBook
    {
        private readonly CatalogContext _context;

        public BookService(CatalogContext context)
        {
            _context = context;
        }

        public Task<PaginatedList<Book>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        //public async Task<BookDetailsViewModel> GetBookDetailsAsync(int bookId, int userId)
        //{
        //    var book = await _bookRepository.GetByIdAsync(bookId);
        //    var vm = new BookDetailsViewModel
        //    {
        //        BookId = book.Id,
        //        Categories = book.BookCategories
        //            .Select(bc => new CategoryViewModel { Id = bc.CategoryId, Name = bc.Category.Name }),
        //        Authors = book.BookAuthors.Select(b => b.Author.Name),
        //        Description = book.Description,
        //        ImageUrl = book.ImageUrlLarge,
        //        IsFavorite = book.Favorites.Any(f => f.UserId == userId),
        //        Price = book.Price,
        //        PublishedDate = book.PublishedDate,
        //        Publisher = book.Publisher.Name,
        //        Title = book.Title,
        //        Reviews = book.Reviews.Select(r => new ReviewViewModel() { Rating = r.Rating, ReviewText = r.ReviewText, UserName = r.User.UserName }),
        //        AverageRating = (decimal?)book.Reviews.Average(r => r.Rating)
        //    };

        //    return vm;
        //}

        public async Task<Book> GetByIdAsync(int bookId)
        {
            return await GetIncludes().SingleOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<PaginatedList<Book>> GetFilteredBooksAsync(
            IEnumerable<int> categories,
            IEnumerable<int> authors,
            IEnumerable<int> publishers,
            int pageSize, int currentPage)
        {
            var query = GetIncludes();

            if (authors.Any())
                query = query.Where(b => b.BookAuthors.Any(ba => authors.Contains(ba.AuthorId)));

            if (categories.Any())
                query = query.Where(b => b.BookCategories.Any(bc => categories.Contains(bc.CategoryId)));

            if (publishers.Any())
                query = query.Where(b => publishers.Contains(b.PublisherId));

            return await PaginatedList<Book>.CreateAsync(query, currentPage, pageSize);

        }

        public Task<int> GetMaxPublishedYearAsync()
        {
            return _context.Books.MaxAsync(b => b.PublishedDate.Year);
        }

        public Task<int> GetMinPublishedYearAsync()
        {
            return _context.Books.MinAsync(b => b.PublishedDate.Year);
        }

        private IQueryable<Book> GetIncludes()
        {
            return  _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.Reviews)
                    .ThenInclude(r => r.User)
                .Include(b => b.Favorites)
                // Note: Might Not need this
                    .ThenInclude(f => f.User)
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .OrderBy(b => b.Title);

        }
    }
}
