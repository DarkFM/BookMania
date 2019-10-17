using System.Collections.Generic;

namespace BookMania.ViewModels
{
    public class BookItemViewModel
    {
        public int BookId { get; set; }
        public string Thumbnail { get; set; }
        public decimal? AverageRating { get; set; }
        public bool IsFavorite { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<string> Authors { get; set; }
    }
}