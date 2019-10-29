using BookMania.Core;
using BookMania.Data.Models;
using BookMania.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookMania.Data.Interfaces
{
    public interface IBook
    {
        Task<Book> GetByIdAsync(int bookId);

        Task<PaginatedList<Book>> GetAllAsync();

        Task<PaginatedList<Book>> GetFilteredBooksAsync(
            IEnumerable<int> categories,
            IEnumerable<int> authors,
            IEnumerable<int> publishers,
            int pageSize, int currentPage
        );

        Task<int> GetMaxPublishedYearAsync();

        Task<int> GetMinPublishedYearAsync();
    }
}
