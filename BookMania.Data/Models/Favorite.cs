namespace BookMania.Data.Models
{
    public class Favorite
    {
        private Favorite()
        {
        }
        
        public Favorite(Book book, ApplicationUser user)
        {
            Book = book;
            User = user;
        }

        public int BookId { get; private set; }
        public Book Book{ get; set; }

        public int UserId { get; private set; }
        public ApplicationUser User { get; set; }
    }
}