using BookMania.Infrastructure.Utils;
using BookMania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.ViewModels
{
    public class ProductListingVM
    {
        public IEnumerable<BookModel> BookItems { get; set; }
        public int TotalItemsFound { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPrevPage { get; set; }
        public IEnumerable<AuthorModel> Authors { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<PublisherModel> Publishers { get; set; }
    }
}
