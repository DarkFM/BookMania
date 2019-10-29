using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.ViewModels
{
    public class FilterResponseViewModel
    {
        [BindProperty(Name = "C")]
        public IEnumerable<int> Categories { get; set; } = new List<int>();

        [BindProperty(Name = "A")]
        public IEnumerable<int> Authors { get; set; } = new List<int>();

        [BindProperty(Name = "P")]
        public IEnumerable<int> Publishers { get; set; } = new List<int>();

        [BindProperty(Name = "Page")]
        public int CurrentPage { get; set; } = 1;

        public void Deconstruct(
            out IEnumerable<int> categories,
            out IEnumerable<int> authors,
            out IEnumerable<int> publishers,
            out int currentPage)
        {
            categories = Categories;
            authors = Authors;
            publishers = Publishers;
            currentPage = CurrentPage;
        }
    }
}