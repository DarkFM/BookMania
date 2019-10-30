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
        private readonly IBook _bookService;
        private readonly IAuthor _authorService;
        private readonly ICategory _categoryService;
        private readonly IPublisher _publisherService;
        private readonly IApplicationUser _userService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IBook bookService,
            IAuthor authorService,
            ICategory categoryService,
            IPublisher publisherService,
            IApplicationUser userService,
            ILogger<ProductsController> logger)
        {
            _logger = logger;
            _bookService = bookService;
            _authorService = authorService;
            _categoryService = categoryService;
            _publisherService = publisherService;
            _userService = userService;
        }

        [HttpGet]
        [SeperatedQueryString]
        public async Task<IActionResult> Index([FromQuery]FilterResponseVM filterModel)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            filterModel.CurrentPage = filterModel.CurrentPage < 1 ? 1 : filterModel.CurrentPage;
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

            AddQueryToViewData(filterModel);
            _logger.LogDebug(string.Join(", ", filterModel.Publishers));

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Favorites(FilterResponseVM filterModel)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            filterModel.CurrentPage = filterModel.CurrentPage < 1 ? 1 : filterModel.CurrentPage;
            var (categories, authors, publishers, currPage) = filterModel;

            var favoriteBooks = await _bookService.GetFilteredBooksAsync(categories, authors, publishers, 30, currPage, userId);
            var allFavoriteBooks = await _bookService.GetFavoriteBooksAsync(userId);

            var bookModels = favoriteBooks.Select(book => book.ToBookModel(true));

            var relatedAuthors = allFavoriteBooks
                .SelectMany(book => book.BookAuthors)
                .Select(ba => ba.Author)
                .Distinct()
                .Select(a => a.ToAuthorModel());

            var relatedCategories = allFavoriteBooks
                .SelectMany(book => book.BookCategories)
                .Select(ba => ba.Category)
                .Distinct()
                .Select(c => c.ToCategoryModel());

            var relatedPublishers = allFavoriteBooks
                .Select(book => book.Publisher)
                .Distinct()
                .Select(p => p.ToPublisherModel());

            var model = new ProductListingVM
            {
                BookItems = bookModels,
                TotalItemsFound = favoriteBooks.TotalItems,
                CurrentPage = favoriteBooks.CurrentPage,
                TotalPages = favoriteBooks.TotalPages,
                HasNextPage = favoriteBooks.HasNextPage,
                HasPrevPage = favoriteBooks.HasPreviousPage,
                Authors = relatedAuthors,
                Categories = relatedCategories,
                Publishers = relatedPublishers
            };

            AddQueryToViewData(filterModel);

            return View(model);
        }

        private void AddQueryToViewData(FilterResponseVM filterModel)
        {
            ViewData["Categories"] = string.Join(",", filterModel.Categories);
            ViewData["Authors"] = string.Join(",", filterModel.Authors);
            ViewData["Publishers"] = string.Join(",", filterModel.Publishers);
        }

        [HttpGet]
        public async Task<IActionResult> Product(int bookId)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            var book = await _bookService.GetByIdAsync(bookId);
            var user = await _userService.GetByIdAsync(userId);

            var isFavorite = user.Favorites.Any(f => f.BookId == book.Id);
            var model = new BookDetailsVM { Book = book.ToBookModel(isFavorite) };

            return View(model);
        }
    }
}
