using BookMania.Data.Models;
using BookMania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Extensions
{
    public static class AuthorExtensions
    {
        public static AuthorModel ToAuthorModel(this Author author, bool isSelected = false)
        {
            return new AuthorModel
            {
                Id = author.Id,
                Name = author.Name,
                IsSelected = isSelected
            };
        }
    }
}
