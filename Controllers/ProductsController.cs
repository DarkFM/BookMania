using BookMania.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICatalogViewModelService _catalogViewModelService;

        public ProductsController(ICatalogViewModelService catalogViewModelService)
        {
            _catalogViewModelService = catalogViewModelService;
        }

        public async Task<IActionResult> Index([FromQuery]FilterResponseViewModel viewModel)
        {
            var catalogViewModel = await _catalogViewModelService.GetFilteredCatalogItemsAsync(0, viewModel.Categories, viewModel.Authors);
            return View(catalogViewModel);
        }

        public class FilterResponseViewModel
        {
            [BindProperty(Name = "Category")]
            public IEnumerable<int> Categories { get; set; } = new List<int>();

            [BindProperty(Name = "Author")]
            public IEnumerable<int> Authors { get; set; } = new List<int>();
        }
    }
}
