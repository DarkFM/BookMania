using BookMania.Core.Entities.BookAggregate;
using BookMania.Core.Interfaces;
using BookMania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Core.Interfaces
{
    public interface IBookRepository : IAsyncRepository<Book>
    {
        Task<PaginatedList<Book>> GetFilteredBooksWithDataAsync(IEnumerable<int> categories, IEnumerable<int> authors, int pageSize, int currentPage);
        Task<int> GetMinPublishedYear();
        Task<int> GetMaxPublishedYear();
    }
}
