using BookMania.Filters;
using BookMania.Interfaces;
using BookMania.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICatalogViewModelService _catalogViewModelService;
        private readonly IBookDetailsService _bookDetailsService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ICatalogViewModelService catalogViewModelService, ILogger<ProductsController> logger, IBookDetailsService bookDetailsService)
        {
            _catalogViewModelService = catalogViewModelService;
            _logger = logger;
            _bookDetailsService = bookDetailsService;
        }

        [HttpGet]
        [SeperatedQueryString]
        public async Task<IActionResult> Index([FromQuery]FilterResponseViewModel viewModel)
        {
            viewModel.CurrentPage = viewModel.CurrentPage < 1 ? 1 : viewModel.CurrentPage;
            var catalogViewModel = await _catalogViewModelService.GetFilteredCatalogItemsAsync(0, viewModel);

            ViewData["Categories"] = string.Join(",", viewModel.Categories);
            ViewData["Authors"] = string.Join(",", viewModel.Authors);
            ViewData["Publishers"] = string.Join(",", viewModel.Publishers);

            return View(catalogViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Product([FromRoute]int id)
        {
            var vm = await _bookDetailsService.GetBookDetailsAsync(id, 0/*HttpContext.User.Identity.*/);
            return View(vm);

        }
    }
}
