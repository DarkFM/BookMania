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
