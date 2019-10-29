using BookMania.Data.Models;
using BookMania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Extensions
{
    public static class CategoryExtensions
    {
        public static CategoryModel ToCategoryModel(this Category category, bool isSelected = false)
        {
            return new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                IsSelected = isSelected
            };
        }
    }
}
