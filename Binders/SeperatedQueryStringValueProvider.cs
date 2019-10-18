using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Binders
{
    // https://www.strathweb.com/2017/07/customizing-query-string-parameter-binding-in-asp-net-core-mvc/
    /// <summary>
    /// Responsible for 
    /// </summary>
    public class SeperatedQueryStringValueProvider : QueryStringValueProvider
    {
        private readonly string _key;
        private readonly string _seperator;
        private readonly IQueryCollection _values;

        /// <summary>
        /// Takes in the query string values and a seperator.
        /// Using this will apply to all applicable parameters
        /// </summary>
        /// <param name="values">The query values</param>
        /// <param name="seperator">The seperator</param>
        public SeperatedQueryStringValueProvider(IQueryCollection values, string seperator)
            :this(null, values, seperator)
        {

        }

        /// <summary>
        /// Takes in the query string values, a seperator and a key.
        /// The key will specify the parameter we want the logic to apply to
        /// </summary>
        /// <param name="key">Parameter name to aply custom binding to</param>
        /// <param name="values">The query values</param>
        /// <param name="seperator">The seperator</param>
        public SeperatedQueryStringValueProvider(string key, IQueryCollection values, string seperator) 
            : base(BindingSource.Query, values, CultureInfo.InvariantCulture)
        {
            _key = key;
            _values = values;
            _seperator = seperator;
        }

        public override ValueProviderResult GetValue(string key)
        {
            // Out of the box, the base will always consider the comma separated values as a single string value.
            var result = base.GetValue(key);

            // exit with base behaviour if the key we specify is not null and doesnt match the set key
            if (_key != null && _key != key)
            {
                return result;
            }

            // perform the split logic if the default binder found a value and the collection uses the seperator
            // takes the comma seperated value from a query and splits it in to a StringValues
            if (result != ValueProviderResult.None &&
                result.Values.Any(x => x.IndexOf(_seperator, StringComparison.OrdinalIgnoreCase) > 0))
            {
                var splitValues = new StringValues(result.Values
                    .SelectMany(x => x.Split(_seperator, StringSplitOptions.None)).ToArray());
                return new ValueProviderResult(splitValues, result.Culture);
            }

            return result;
        }
    }
}
