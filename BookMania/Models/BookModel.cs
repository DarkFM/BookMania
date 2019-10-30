using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public PublisherModel Publisher { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlLarge { get; set; }
        public bool IsFavorite { get; set; }
        public decimal? AverageRating { get; internal set; }

        public IEnumerable<AuthorModel> Authors { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<ReviewModel> Reviews { get; set; }
    }
}
