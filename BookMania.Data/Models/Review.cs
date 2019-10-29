namespace BookMania.Data.Models
{
    public class Review
    {
        public int BookId { get; private set; }
        public Book Book { get; set; }
        public int UserId { get; private set; }
        public ApplicationUser User { get; set; }

        public int? Rating { get; set; }
        public string ReviewText { get; set; }
    }
}
