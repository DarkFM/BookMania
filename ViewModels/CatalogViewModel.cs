using BookMania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.ViewModels
{
    public class CatalogViewModel
    {
        public IEnumerable<BookItemViewModel> BookItems { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<PublisherViewModel> Publishers { get; set; }
        public IEnumerable<AuthorViewModel> Authors { get; set; }
        public int MaxYear { get; set; }
        public int MinYear { get; set; }
        public int TotalItemsFound { get; set; }
    }
}
