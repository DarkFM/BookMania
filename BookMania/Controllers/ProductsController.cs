using BookMania.Data.Interfaces;
using BookMania.Infrastructure.Utils;
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
using BookMania.Extensions;

namespace BookMania.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IBook _bookService;
        private readonly IAuthor _authorService;
        private readonly ICategory _categoryService;
        private readonly IPublisher _publisherService;

        public ProductsController(
            ILogger<ProductsController> logger,
            IBook bookService,
            IAuthor authorService,
            ICategory categoryService,
            IPublisher publisherService)
        {
            _logger = logger;
            _bookService = bookService;
            _authorService = authorService;
            _categoryService = categoryService;
            _publisherService = publisherService;
        }

        [HttpGet]
        [SeperatedQueryString]
        public async Task<IActionResult> Index([FromQuery]FilterResponseViewModel filterModel)
        {
            filterModel.CurrentPage = filterModel.CurrentPage < 1 ? 1 : filterModel.CurrentPage;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            var (categories, authors, publishers, currPage) = filterModel;

            var paginatedBooks = await _bookService.GetFilteredBooksAsync(categories, authors, publishers, 30, currPage);
            var bookModels = paginatedBooks.Select(book => book.ToBookModel(book.Favorites.Any(f => f.UserId == userId)));
            var allAuthors = _authorService.GetAll().Select(a => a.ToAuthorModel(filterModel.Authors.Contains(a.Id)));
            var allPublishers = _publisherService.GetAll().Select(p => p.ToPublisherModel(filterModel.Publishers.Contains(p.Id)));
            var allCategories = _categoryService.GetAll().Select(c => c.ToCategoryModel(filterModel.Categories.Contains(c.Id)));

            var model = new ProductListingVM
            {
                BookItems = bookModels,
                TotalItemsFound = paginatedBooks.TotalItems,
                CurrentPage = paginatedBooks.CurrentPage,
                TotalPages = paginatedBooks.TotalPages,
                HasNextPage = paginatedBooks.HasNextPage,
                HasPrevPage = paginatedBooks.HasPreviousPage,
                Authors = allAuthors,
                Categories = allCategories,
                Publishers = allPublishers
            };


            ViewData["Categories"] = string.Join(",", filterModel.Categories);
            ViewData["Authors"] = string.Join(",", filterModel.Authors);
            ViewData["Publishers"] = string.Join(",", filterModel.Publishers);
            _logger.LogDebug(string.Join(", ", filterModel.Publishers));

            return View(model);
        }

        [HttpGet]
        //[Authorize]
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
