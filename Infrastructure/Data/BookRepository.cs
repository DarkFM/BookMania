using BookMania.Core;
using BookMania.Core.Entities.BookAggregate;
using BookMania.Core.Interfaces;
using BookMania.Infrastructure.Utils;
using BookMania.Interfaces;
using BookMania.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Infrastructure.Data
{
    public class BookRepository : EfRepository<Book>, IBookRepository
    {
        public BookRepository(CatalogContext dbContext)
            : base(dbContext)
        {

        }

        public Task<PaginatedList<Book>> GetFilteredBooksWithDataAsync(
            IEnumerable<int> categories,
            IEnumerable<int> authors,
            IEnumerable<int> publishers,
            int pageSize, int currentPage)
        {
            IQueryable<Book> booksFromDb = _dbContext.Books
                .AsNoTracking()
                .Include(b => b.Publisher)
                .Include(b => b.Reviews)
                .Include(b => b.Favorites)
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category);

            if (authors.Any())
                booksFromDb = booksFromDb.Where(b => b.BookAuthors.Any(ba => authors.Contains(ba.AuthorId)));

            if (categories.Any())
                booksFromDb = booksFromDb.Where(b => b.BookCategories.Any(bc => categories.Contains(bc.CategoryId)));

            if (publishers.Any())
                booksFromDb = booksFromDb.Where(b => publishers.Contains(b.PublisherId));

            return PaginatedList<Book>.CreateAsync(booksFromDb, currentPage, pageSize, b => b.Title);
        }

        public async override Task<Book> GetByIdAsync(int id)
        {
            // Expliceityl loading the required data
            var book = await base.GetByIdAsync(id);

            // https://stackoverflow.com/a/49968809
            // might be a bug in ef core, but this will fix this issue apparently (include the calling end of the relationship)
            _dbContext.Entry(book).Collection(b => b.BookAuthors).Query()
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .Load();

            _dbContext.Entry(book).Collection(b => b.BookCategories).Query()
                .Include(bc => bc.Book)
                .Include(bc => bc.Category)
                .Load();

            _dbContext.Entry(book)
                .Collection(b => b.Favorites)
                .Load();

            _dbContext.Entry(book)
                .Collection(b => b.Reviews)
                .Query()
                .Include(r => r.Book)
                .Include(r => r.User)
                .Load();

            _dbContext.Entry(book)
                .Reference(b => b.Publisher)
                .Load();

            return book;
        }

        public Task<int> GetMaxPublishedYear()
        {
            return _dbContext.Books.MaxAsync(b => b.PublishedDate.Year);
        }

        public Task<int> GetMinPublishedYear()
        {
            return _dbContext.Books.MinAsync(b => b.PublishedDate.Year);
        }
    }
}
