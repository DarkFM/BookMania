using BookMania.Core.Extensions;
using System;

namespace BookMania.Core.Entities.OrderAggregate
{
    public class Address // Value Object
    {
        private Address()
        {
        }

        public Address(string country, string countryProvince, string zipOrPostCode, string addressLine1, string addressLine2 = default, string addressLine3 = default)
        {
            country.ThrowIfNullOrWhiteSpace($"{nameof(country)} is invalid");
            countryProvince.ThrowIfNullOrWhiteSpace($"{nameof(countryProvince)} is invalid");
            zipOrPostCode.ThrowIfNullOrWhiteSpace($"{nameof(zipOrPostCode)} is invalid");
            addressLine1.ThrowIfNullOrWhiteSpace($"{nameof(addressLine1)} is invalid");

            Country = country;
            CountryProvince = countryProvince;
            ZipOrPostCode = zipOrPostCode;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
        }

        public string Country { get; set; }
        public string CountryProvince { get; set; }
        public string ZipOrPostCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
    }
}
