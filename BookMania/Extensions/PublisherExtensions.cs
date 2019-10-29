using BookMania.Data.Models;
using BookMania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Extensions
{
    public static class PublisherExtensions
    {
        public static PublisherModel ToPublisherModel(this Publisher publisher, bool isSelected = false)
        {
            return new PublisherModel
            {
                Id = publisher.Id,
                Name = publisher.Name,
                IsSelected = isSelected
            };
        }
    }
}
