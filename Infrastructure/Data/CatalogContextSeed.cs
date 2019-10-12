using BookMania.Extensions;
using BookMania.Infrastructure.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookMania.Infrastructure.Data
{
    public class CatalogContextSeed
    {
        private readonly HttpClient client;
        private readonly ILogger<CatalogContextSeed> logger;
        private readonly GoogleApiOptions apiOptions;
        private readonly ICollection<string> _catageories = new List<string> { 
            "History", "Business & Economics", "Biography & Autobiography", "Philosophy", "Political Science", 
            "Action and adventure", "Romance", "Medical", "Computers", "Cooking", "Art", "Folklore",
            "Education", "Horror", "Graphic novel", "Mystery", "Romance", "Thriller", "Biography"
        };

        public CatalogContextSeed(HttpClient httpClient, IOptionsMonitor<GoogleApiOptions> options, ILogger<CatalogContextSeed> logger)
        {
            apiOptions = options.CurrentValue;

            httpClient.BaseAddress = new Uri(apiOptions.BaseUrl);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            client = httpClient;
            this.logger = logger;
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
            var (int1, int2) = (rand.Next(25) + 97, rand.Next(25) + 97);
            string randTwoLetter = char.ConvertFromUtf32(int1) + char.ConvertFromUtf32(int2);

            // using partial response
            // https://www.googleapis.com/books/v1/volumes?q=flowers+inauthor:keyes&fields=items(id,volumeInfo(title,authors,publisher,publishedDate,description,categories,imageLinks),saleInfo(retailPrice))
            // https://developers.google.com/books/docs/v1/performance.html#partial-response
            var queryString = $"?key={apiOptions.ApiKey}"
                .AddQuery("fields", "items(id,volumeInfo(title,authors,publisher,publishedDate,description,categories,imageLinks),saleInfo(retailPrice))")
                //.AddQuery("q", randTwoLetter)
                .AddQuery("q", $"subject:{category}")
                .AddQuery("filter", "paid-ebooks")
                .AddQuery("printType", "books")
                .AddQuery("langRestrict", "en")
                .AddQuery("maxResults", "20");

            var httpResponse = await client.GetAsync(queryString);
            var data = await httpResponse.Content.ReadAsAsync<GoogleResponse>();
            SaveCategoriesToFile(data);


            return data;
        }

        private HashSet<string> SaveUniqueCategories(IEnumerable<string> categories)
        {
            HashSet<string> uniqueCategories = new HashSet<string>();

            foreach(var category in categories)
            {
                uniqueCategories.Add(category.ToLower());
            }

            return uniqueCategories;
        }

        private void SaveCategoriesToFile(GoogleResponse data)
        {
            IEnumerable<string> categories = data.Items.SelectMany(x => x.VolumeInfo.Categories);
            //var result = SaveUniqueCategories(categories);
            IEnumerable<(string category, string title)> result = data.Items.Select(x => (string.Join(", ", x.VolumeInfo.Categories), x.VolumeInfo.Title));
            var strBuilder = new StringBuilder();
            foreach (var (category, title) in result)
            {

                strBuilder.Append("Category: ").Append(category).Append(", Title: ").AppendLine(title).AppendLine();
                logger.LogDebug($"Category: {category}, Title: {title}");
            }

            File.AppendAllText(@"D:\Categories.txt", strBuilder.ToString());
        }
    }

    public class GoogleResponse
    {
        public IEnumerable<ProductInfo> Items { get; set; } = new List<ProductInfo>();
    }

    public class ProductInfo
    {
        public string Id { get; set; }
        public Volumeinfo VolumeInfo { get; set; }
        public Saleinfo SaleInfo { get; set; }
    }

    public class Volumeinfo
    {
        public string Title { get; set; }
        public IEnumerable<string> Authors { get; set; } = new List<string>();
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
        public string Description { get; set; }

        // Allows Json.Net to populate this as it will create a new object, rather than reusing the existing one
        public IEnumerable<string> Categories { get; set; } = new List<string>() { "Unknown" }.AsReadOnly();
        public Imagelinks ImageLinks { get; set; }
    }

    public class Imagelinks
    {
        public string SmallThumbnail { get; set; }
        public string Thumbnail { get; set; }
    }

    public class Saleinfo
    {
        public Retailprice RetailPrice { get; set; }
    }

    public class Retailprice
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
    }


}
