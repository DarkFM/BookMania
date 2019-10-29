using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace BookMania.Core.Extensions
{
    public static class StringExtensions
    {
        public static void ThrowIfNullOrWhiteSpace(this string source, string message = default)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentException(message ?? "String is null/empty not valid");
        }
        public static string AddQuery(this string source, string key, string value)
        {
            string baseUrl = "", queryParams = "";

            source = source.TrimEnd('&');
            string[] splitUrl = source.Split('?', StringSplitOptions.RemoveEmptyEntries);

            if (splitUrl.Length == 2)
            {
                (baseUrl, queryParams) = (splitUrl[0], splitUrl[1]);
            }
            else if (splitUrl.Length == 1)
            {
                // source is a query string starting with '?'
                if (splitUrl[0].Contains('='))
                    (baseUrl, queryParams) = ("", splitUrl[0]);
                // source is a http Url
                else
                    (baseUrl, queryParams) = (splitUrl[0], "");
            }

            NameValueCollection queryCollection = HttpUtility.ParseQueryString(queryParams);
            queryCollection.Remove(key);
            queryCollection.Add(key, value);

            var newSource = new StringBuilder(baseUrl).Append('?');

            // add query params to base Url
            foreach (var k in queryCollection.AllKeys)
            {
                string paramVal = queryCollection[k];
                newSource.Append(k).Append("=").Append(paramVal).Append("&");
            }

            // remove trailing '&'
            return newSource.ToString().TrimEnd('&');
        }
    }
}
