using BookMania.Filters;
using BookMania.Interfaces;
using BookMania.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookMania.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [SeperatedQueryString]
        public async Task<IActionResult> Index([FromQuery]FilterResponseViewModel viewModel)
        {
            //viewModel.CurrentPage = viewModel.CurrentPage < 1 ? 1 : viewModel.CurrentPage;
            //var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //int.TryParse(userId, out int id);
            //var catalogViewModel = await _catalogViewModelService.GetFilteredCatalogItemsAsync(viewModel, id);

            //ViewData["Categories"] = string.Join(",", viewModel.Categories);
            //ViewData["Authors"] = string.Join(",", viewModel.Authors);
            //ViewData["Publishers"] = string.Join(",", viewModel.Publishers);
            //_logger.LogDebug(string.Join(", ", viewModel.Publishers));

            //return View(catalogViewModel);
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Favorites(FilterResponseViewModel viewModel)
        {
            //viewModel.CurrentPage = viewModel.CurrentPage < 1 ? 1 : viewModel.CurrentPage;

            //ViewData["Categories"] = string.Join(",", viewModel.Categories);
            //ViewData["Authors"] = string.Join(",", viewModel.Authors);
            //ViewData["Publishers"] = string.Join(",", viewModel.Publishers);
            ////_logger.LogDebug(string.Join(", ", viewModel.Publishers));

            //var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //int.TryParse(userId, out int id);
            //var model = await userService.GetFavoritesAsync(viewModel, id);
            //return View(model);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Product(int bookId)
        {
            //var userIdStr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //int.TryParse(userIdStr, out int userId);
            //var vm = await _bookDetailsService.GetBookDetailsAsync(bookId, userId);
            //return View(vm);
            return View();

        }
    }
}
