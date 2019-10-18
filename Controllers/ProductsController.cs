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
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ICatalogViewModelService catalogViewModelService, ILogger<ProductsController> logger)
        {
            _catalogViewModelService = catalogViewModelService;
            _logger = logger;
        }

        public async Task<IActionResult> Index([FromQuery]FilterResponseViewModel viewModel)
        {
            viewModel.CurrentPage = viewModel.CurrentPage < 1 ? 1 : viewModel.CurrentPage;
            var catalogViewModel = await _catalogViewModelService.GetFilteredCatalogItemsAsync(0, viewModel);
            var query = Request.Query.ToDictionary(k => k.Key, v => v.Value.ToString());
            query.Remove("page");

            ViewData["QueryParams"] = query;
            foreach (var kvp in query)
            {
                _logger.LogDebug($"Key: {kvp.Key}, Value: {kvp.Value}");
            }
            return View(catalogViewModel);
        }
    }
}
