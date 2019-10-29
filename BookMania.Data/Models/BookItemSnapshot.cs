namespace BookMania.Data.Models
{
    /// <summary>
    /// Represents a snapshot of the book when an order was made
    /// </summary>
    public class BookItemSnapshot
    {
        private BookItemSnapshot()
        {
        }

        // https://docs.microsoft.com/en-us/ef/core/modeling/constructors
        public BookItemSnapshot(int bookId, string title, string imageUrl = default)
        {
            BookId = bookId;
            ImageUrl = imageUrl;
            Title = title;
        }


        public int BookId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
