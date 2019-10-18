using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Binders
{
    // https://www.strathweb.com/2017/07/customizing-query-string-parameter-binding-in-asp-net-core-mvc/
    /// <summary>
    /// Factory responsible for providing instances of value providers
    /// Used by registering a value provider with MVC
    /// </summary>
    public class SeperatedQueryStringProviderFactory : IValueProviderFactory
    {
        private readonly string _seperator;
        private readonly string _key;

        public SeperatedQueryStringProviderFactory(string seperator)
            : this(null, seperator)
        {

        }

        public SeperatedQueryStringProviderFactory(string key, string seperator)
        {
            _key = key;
            _seperator = seperator;
        }

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            var queryCollection = context.ActionContext.HttpContext.Request.Query;
            context.ValueProviders.Insert(0, new SeperatedQueryStringValueProvider(_key, queryCollection, _seperator));
            return Task.CompletedTask;
        }
    }
}
