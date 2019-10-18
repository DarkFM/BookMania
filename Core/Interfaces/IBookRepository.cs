using BookMania.Core.Entities.BookAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMania.Core.Interfaces
{
    public interface IBookRepository : IAsyncRepository<Book>
    {
        Task<PaginatedList<Book>> GetFilteredBooksWithDataAsync(IEnumerable<int> categories, IEnumerable<int> authors, IEnumerable<int> publishers, int pageSize, int currentPage);
        Task<int> GetMinPublishedYear();
        Task<int> GetMaxPublishedYear();
    }
}
