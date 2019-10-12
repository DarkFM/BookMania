using BookMania.Core.Entities.OrderAggregate;
using BookMania.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Core.Entities.UserAggregate
{
    public class PaymentDetails // ValueObject
    {
        private PaymentDetails()
        {
        }

        public PaymentDetails(int userId, string nameOnCard, byte[] cardNumberHash, byte[] cardSecurityKeyHash, byte[] salt, int last4,
                              Address billingAddress, bool isDefault = false)
        {
            salt.ThrowIfNullOrEmpty($"{nameof(salt)} cannot be null/empty");
            nameOnCard.ThrowIfNullOrWhiteSpace($"{nameof(nameOnCard)} cannot be null/empty");
            cardNumberHash.ThrowIfNullOrEmpty($"{nameof(cardNumberHash)} cannot be null/empty");
            cardSecurityKeyHash.ThrowIfNullOrEmpty($"{nameof(cardSecurityKeyHash)} cannot be null/empty");
            billingAddress.ThrowIfNull($"{nameof(billingAddress)} cannot be null");

            if (last4 < 1000 || last4 > 9999)
                throw new ArgumentOutOfRangeException($"Last 4 digits are invalid");

            UserId = userId;
            CardNumberHash = cardNumberHash;
            CardSecurityKeyHash = cardSecurityKeyHash;
            Salt = salt;
            Last4 = last4;
            IsDefault = isDefault;
            BillingAddress = billingAddress;
            NameOnCard = nameOnCard;
        }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public byte[] CardNumberHash { get; set; }
        public byte[] CardSecurityKeyHash { get; set; }
        public byte[] Salt { get; set; }
        public int Last4 { get; set; }
        public bool IsDefault { get; set; }
        public Address BillingAddress { get; set; }
        public string NameOnCard { get; set; }
    }
}
