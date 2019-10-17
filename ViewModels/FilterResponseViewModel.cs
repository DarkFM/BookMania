using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.ViewModels
{
    public class FilterResponseViewModel
    {
        [BindProperty(Name = "Category")]
        public IEnumerable<int> Categories { get; set; } = new List<int>();

        [BindProperty(Name = "Author")]
        public IEnumerable<int> Authors { get; set; } = new List<int>();

        [BindProperty(Name = "Publisher")]
        public IEnumerable<int> Publishers { get; set; } = new List<int>();

        public void Deconstruct(
            out IEnumerable<int> categories,
            out IEnumerable<int> authors,
            out IEnumerable<int> publishers)
        {
            categories = Categories;
            authors = Authors;
            publishers = Publishers;
        }
    }
}
