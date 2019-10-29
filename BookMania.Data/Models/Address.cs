namespace BookMania.Data.Models
{
    public class Address
    {
        private Address()
        {
        }

        public Address(string country, string countryProvince, string zipOrPostCode, string addressLine1, string addressLine2 = default, string addressLine3 = default)
        {
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
