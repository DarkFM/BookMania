using BookMania.Core.Entities.BookAggregate;

namespace BookMania.Core.Entities.UserAggregate
{
    public class Favorite
    {
        private Favorite()
        {
        }
        
        public Favorite(int bookId, int userId)
        {
            BookId = bookId;
            UserId = userId;
        }

        public int BookId { get; set; }
        public Book Book{ get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}