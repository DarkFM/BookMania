using BookMania.Core.Entities;
using BookMania.Core.Entities.BookAggregate;
using BookMania.Extensions;
using BookMania.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookMania.Infrastructure.Data
{
    public class CatalogContextSeed
    {
        private readonly HttpClient client;
        private readonly CatalogContext ctx;
        private readonly GoogleApiOptions apiOptions;
        private readonly ICollection<string> _catageories = new List<string> {
            "Drama", "Philosophy", "Fantasy", "Satire", "Suspense", "Young Adult Fiction", "Action And Adventure",
            "Romance", "Art", "Folklore","Horror", "Graphic Novel", "Mystery", "Adult Romance ", "Thriller",
            "Biography", "Science Fiction", "New Adult", "Westerns"
        };

        public CatalogContextSeed(HttpClient httpClient, IOptionsMonitor<GoogleApiOptions> options, CatalogContext ctx)
        {
            apiOptions = options.CurrentValue;

            httpClient.BaseAddress = new Uri(apiOptions.BaseUrl);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            client = httpClient;
            this.ctx = ctx;
        }

        public async Task GetBooks()
        {
            foreach (var category in _catageories)
            {
                await GetRandomBooksAsync(category.ToLower());
            }
        }

        public async Task<GoogleResponse> GetRandomBooksAsync(string category = "")
        {
            var rand = new Random();
            // [a, m], [a, t]
            var (int1, int2) = (rand.Next(20) + 97, rand.Next(20) + 97);
            string randTwoLetter = "";//char.ConvertFromUtf32(int1) + char.ConvertFromUtf32(int2);

            // using partial response
            // https://www.googleapis.com/books/v1/volumes?q=flowers+inauthor:keyes&fields=items(id,volumeInfo(title,authors,publisher,publishedDate,description,categories,imageLinks),saleInfo(retailPrice))
            // https://developers.google.com/books/docs/v1/performance.html#partial-response
            var queryString = $"?key={apiOptions.ApiKey}"
                .AddQuery("fields", "items(id,volumeInfo(title,authors,publisher,publishedDate,description,categories,imageLinks),saleInfo(retailPrice))")
                .AddQuery("q", $"{randTwoLetter}+subject:{Uri.EscapeDataString(category)}")
                .AddQuery("filter", "paid-ebooks")
                .AddQuery("printType", "books")
                .AddQuery("langRestrict", "en")
                .AddQuery("maxResults", "40")
                .AddQuery("startIndex", rand.Next(100).ToString());


            var httpResponse = await client.GetAsync(queryString);
            return await httpResponse.Content.ReadAsAsync<GoogleResponse>();
        }


        public async Task SeedDataAsync()
        {
            if (ctx.Books.Any())
                return;

            // loop through catagories and get books for each
            foreach (var category in _catageories)
            {

                var response = await GetRandomBooksAsync(category);

                foreach (var productInfo in response.Items)
                {
                    var (_, volumeInfo, salesInfo) = productInfo;

                    // VolumeInfo Details
                    var (bookTitle, bookPublisher, bookDescription, publishedDate) = volumeInfo;
                    var (authors, categories, (_, thumbnail)) = volumeInfo;
                    var largeImage = thumbnail.AddQuery("zoom", "3"); // enables medium image size

                    // SalesInfo Details
                    var (price, currency) = salesInfo.RetailPrice;


                    // *********** Saving data to database ****************

                    var publisher = new Publisher(bookPublisher.ToLowerInvariant());
                    AddToDb(publisher, p => p.Name == publisher.Name);
                    publisher = GetFromDb<Publisher>(p => p.Name == publisher.Name);

                    var book = new Book(bookTitle, price, publishedDate, publisher, bookDescription, thumbnail, largeImage);
                    AddToDb(book, b => b.Title == book.Title && b.PublishedDate == publishedDate);
                    book = GetFromDb<Book>(b => b.Title == book.Title && b.PublishedDate == publishedDate);

                    var authorsObj = authors.Select(ba => new Author(ba.Trim().ToLowerInvariant()));
                    foreach (var author in authorsObj)
                    {
                        AddToDb(author, a => a.Name == author.Name.ToLowerInvariant());
                    }
                    authorsObj = authorsObj.Select(author => GetFromDb<Author>(a => a.Name == author.Name));

                    var bookAuthors2 = authorsObj.Select(author => new BookAuthor() { Author = author, Book = book });
                    foreach (var bookAuthor in bookAuthors2)
                    {
                        AddToDb(bookAuthor);
                    }

                    var categoriesObj = categories.Select(c => new Category(c.ToLowerInvariant()));
                    foreach (var category2 in categoriesObj)
                    {
                        AddToDb(category2, c => c.Name == category2.Name);
                    }
                    categoriesObj = categoriesObj.Select(cat => GetFromDb<Category>(c => c.Name == cat.Name));

                    var bookCategories = categoriesObj.Select(cat => new BookCategory() { Category = cat, Book = book });
                    foreach (var bookCategory in bookCategories)
                    {
                        AddToDb(bookCategory);
                    }
                }

            }

        }

        /// <summary>
        /// Saves the data to the database if it has not already been added
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="data">The data is passed as a ref so that it will contain the database value if the object alreadt exists.
        /// This prevents adding duplicate values
        /// </param>
        /// <param name="checkIfExists">The predicate expression used in the LINQ query to check for existing values</param>
        private void AddToDb<T>(T data, Expression<Func<T, bool>> checkIfExists = default) where T : class
        {
            // Check if the entity already exists in the database
            Expression<Func<T, bool>> predicate = checkIfExists ?? (_ => false);

            var existingType = ctx.Set<T>().FirstOrDefault(predicate);
            if (existingType == null)
            {
                var result = ctx.Set<T>().Add(data);
                if (result.State == EntityState.Added)
                {
                    ctx.SaveChanges();
                }
            }
        }

        private T GetFromDb<T>(Expression<Func<T, bool>> criteria = default) where T : class
        {
            return ctx.Set<T>().AsNoTracking().FirstOrDefault(criteria);
        }
    }

    #region ResponseObjects
    public class GoogleResponse
    {
        public IEnumerable<ProductInfo> Items { get; set; } = new List<ProductInfo>();
    }

    public class ProductInfo
    {
        public string Id { get; set; }
        public Volumeinfo VolumeInfo { get; set; } = new Volumeinfo();
        public Saleinfo SaleInfo { get; set; } = new Saleinfo();

        public void Deconstruct(out string id, out Volumeinfo volumeinfo, out Saleinfo saleinfo)
        {
            id = Id;
            volumeinfo = VolumeInfo;
            saleinfo = SaleInfo;
        }
    }

    public class Volumeinfo
    {
        public string Title { get; set; }
        public IEnumerable<string> Authors { get; set; } = new List<string>();
        public string Publisher { get; set; }

        [JsonConverter(typeof(DateConverter))]
        public DateTime PublishedDate { get; set; }

        public string Description { get; set; }

        // Allows Json.Net to populate this as it will create a new object, rather than reusing the existing one
        public IEnumerable<string> Categories { get; set; } = new List<string>() { "Unknown" }.AsReadOnly();
        public Imagelinks ImageLinks { get; set; } = new Imagelinks();

        public void Deconstruct(out string title, out string publisher, out string description, out DateTime publishedDate)
        {
            title = Title;
            publisher = Publisher;
            description = Description;
            publishedDate = PublishedDate;
        }

        public void Deconstruct(out IEnumerable<string> authors, out IEnumerable<string> categories, out Imagelinks imagelinks)
        {
            authors = Authors;
            categories = Categories;
            imagelinks = ImageLinks;
        }
    }

    public class Imagelinks
    {
        public string SmallThumbnail { get; set; }
        public string Thumbnail { get; set; }

        public void Deconstruct(out string smallThumbnail, out string largeThumbnail)
        {
            smallThumbnail = SmallThumbnail;
            largeThumbnail = Thumbnail;
        }
    }

    public class Saleinfo
    {
        public Retailprice RetailPrice { get; set; } = new Retailprice();
    }

    public class Retailprice
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }

        public void Deconstruct(out decimal amount, out string currencyCode)
        {
            amount = Amount;
            currencyCode = CurrencyCode;
        }
    }

    #endregion

    #region CustomConverters
    public class DateConverter : DateTimeConverterBase
    {
        public override bool CanWrite => false;
        /// <summary>
        /// Responsisble for Deserializing the JSON value into a .NET type
        /// </summary>
        /// <param name="reader">Provides access to the JSON Data</param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer">Serilizer used to serialize/deserialize the JSON</param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (string.IsNullOrWhiteSpace((string)reader.Value))
            {
                return default(DateTime);
            }

            var date = reader.Value.ToString();

            var dateParts = date.Split('-');
            int year, month = 1, day = 1;

            year = int.Parse(dateParts[0]);

            if (dateParts.Length == 2)
            {
                month = int.Parse(dateParts[1]);
            }
            if (dateParts.Length == 3)
            {
                day = int.Parse(dateParts[2]);
            }

            return new DateTime(year, month, day);
        }

        // Will use default instead. so this wont be called. (because of override on CanWrite => false)
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

}
