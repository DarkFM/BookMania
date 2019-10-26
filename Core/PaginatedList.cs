using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookMania.Core
{
    /// <summary>
    /// Object that holds a paginated list of <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">The type of list</typeparam>
    public class PaginatedList<T> : List<T>
    {
        /// <summary>
        /// Stores the current page.
        /// </summary>
        public int CurrentPage { get; }

        /// <summary>
        /// Stores the total pages of the list.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// The total items in the original list
        /// </summary>
        public int TotalItems { get; }

        public PaginatedList(List<T> items, int totalItemCount, int pageIndex, int pageSize)
        {
            CurrentPage = pageIndex;
            TotalPages = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            TotalItems = totalItemCount;

            this.AddRange(items);
        }

        public bool HasPreviouspage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        /// <summary>
        /// Creates a new paged list of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="source">The IQueryable source of data.</param>
        /// <param name="pageIndex">The current page index.</param>
        /// <param name="pageSize">The total items shown per page.</param>
        /// <returns></returns>
        public static Task<PaginatedList<T>> CreateAsync<TSort>(IQueryable<T> source, int pageIndex, int pageSize, Expression<Func<T, TSort>> orderBy)
        {
            var count = source.Count();
            var numberItemsToSkip = pageSize * (pageIndex - 1);

            var items = source
                .OrderBy(orderBy)
                .Skip(numberItemsToSkip)
                .Take(pageSize)
                .ToList();

            //var count = await countTask;
            //var items = await itemsTask;

            return Task.FromResult(new PaginatedList<T>(items, count, pageIndex, pageSize));
        }
    }
}
