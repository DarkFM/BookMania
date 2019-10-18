using BookMania.Binders;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class SeperatedQueryStringAttribute : Attribute, IAsyncResourceFilter
    {
        private readonly SeperatedQueryStringProviderFactory _factory;

        public SeperatedQueryStringAttribute()
            : this(",")
        {

        }

        public SeperatedQueryStringAttribute(string seperator)
        {
            _factory = new SeperatedQueryStringProviderFactory(seperator);
        }

        public SeperatedQueryStringAttribute(string seperator, string key)
        {
            _factory = new SeperatedQueryStringProviderFactory(key, seperator);
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            // OnResourceExecuting
            // inject the value provider before the action filter runs
            context.ValueProviderFactories.Insert(0, _factory);
            
            await next();

            // OnResouceExecuted
            //...
        }
    }
}
