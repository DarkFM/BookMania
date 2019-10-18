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

            var queryString = Request.QueryString.Value;
            var filteredQueryStrings = queryString.Split('&').Where(x => !x.Contains("page", StringComparison.OrdinalIgnoreCase));
            var finalQueryString = string.Join('&', filteredQueryStrings).TrimStart('?').Insert(0, "?");

            ViewData["QueryParams"] = finalQueryString;
            _logger.LogDebug(queryString);
            _logger.LogDebug(finalQueryString);
            return View(catalogViewModel);
        }
    }
}
