using System;

namespace BookMania.Data.Models
{
    public class PaymentDetails
    {
        private PaymentDetails()
        {
        }

        public PaymentDetails(int userId, string nameOnCard, byte[] cardNumberHash, byte[] cardSecurityKeyHash, byte[] salt, int last4,
                              Address billingAddress, bool isDefault = false)
        {
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
