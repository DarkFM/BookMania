using BookMania.Core.Entities.BookAggregate;
using BookMania.Core.Entities.OrderAggregate;
using BookMania.Core.Extensions;
using BookMania.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace BookMania.Core.Entities.UserAggregate
{
    public class ApplicationUser : IdentityUser<int>, IAggregateRoot
    {
        private readonly List<PaymentDetails> _paymentOptions = new List<PaymentDetails>();
        private readonly List<Favorite> _favorites = new List<Favorite>();

        private ApplicationUser()
        {
        }

        public ApplicationUser(string firstName, string lastName, List<PaymentDetails> paymentOptions = default)
        {
            lastName.ThrowIfNullOrWhiteSpace($"{nameof(lastName)} cannot be null/white-space");
            firstName.ThrowIfNullOrWhiteSpace($"{nameof(firstName)} cannot be null/white-space");

            FirstName = firstName;
            LastName = lastName;
            _paymentOptions = paymentOptions;
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

        public void AddFavouriteBook(int bookId)
        {
            if (!_favorites.Any(f => f.BookId == bookId))
            {
                _favorites.Add(new Favorite(bookId, this.Id));
            }
        }

        public void RemoveFavoriteBook(int bookId)
        {
            var favoriteBook = _favorites.Find(f => f.BookId == bookId);
            _favorites.Remove(favoriteBook);
            //_favorites.RemoveAll(f => f.BookId == bookId);
        }
    }
}
