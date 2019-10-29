using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace BookMania.Data.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        private readonly List<PaymentDetails> _paymentOptions = new List<PaymentDetails>();
        private readonly List<Favorite> _favorites = new List<Favorite>();

        private ApplicationUser()
        {
        }

        public ApplicationUser(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Address ShippingAddress{ get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public IReadOnlyCollection<Favorite> Favorites => _favorites.AsReadOnly();
        public IReadOnlyCollection<PaymentDetails> PaymentOptions => _paymentOptions.AsReadOnly();

        public void AddNewPayment(PaymentDetails paymentDetails)
        {
            _paymentOptions.Add(paymentDetails);
        }

        public void AddFavouriteBook(Book book)
        {
            if (!_favorites.Any(f => f.BookId == book.Id))
            {
                _favorites.Add(new Favorite(book, this));
            }
        }

        public void RemoveFavoriteBook(int bookId)
        {
            var favoriteBook = _favorites.Find(f => f.BookId == bookId);
            _favorites.Remove(favoriteBook);
        }
    }
}
