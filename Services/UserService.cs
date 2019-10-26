using BookMania.Core;
using BookMania.Core.Entities.BookAggregate;
using BookMania.Core.Interfaces;
using BookMania.Interfaces;
using BookMania.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CatalogViewModel> GetFavoritesAsync(FilterResponseViewModel responseFilters, int userId, int pageSize = 30)
        {

            if (pageSize < 1) pageSize = 30;

            var (categories, authors, publishers, pageIndex) = responseFilters;
            var favs = await _userRepository.GetFavoritesAsync(userId);
            var books = favs.Select(f => f.Book).AsQueryable();

            var minYear = books.Min(b => b.PublishedDate.Year);
            var maxYear = books.Max(b => b.PublishedDate.Year);

            var allPublishers = books.Select(b => b.Publisher).ToList();
            var allCategories = books.SelectMany(b => b.BookCategories).Select(bp => bp.Category).ToHashSet().ToList();
            var allAuthors = books.SelectMany(b => b.BookAuthors).Select(ba => ba.Author).ToHashSet().ToList();

            if (authors.Any())
                books = books.Where(b => b.BookAuthors.Any(ba => authors.Contains(ba.AuthorId)));

            if (categories.Any())
                books = books.Where(b => b.BookCategories.Any(bc => categories.Contains(bc.CategoryId)));

            if (publishers.Any())
                books = books.Where(b => publishers.Contains(b.PublisherId));

            var paginatedBooks = await PaginatedList<Book>.CreateAsync(books, pageIndex, pageSize, orderBy: b => b.Title);


            var vm = new CatalogViewModel
            {
                BookItems = paginatedBooks.Select(book => new BookDetailsViewModel
                {
                    Authors = book.BookAuthors.Select(ba => ba.Author.Name),
                    BookId = book.Id,
                    Description = book.Description,
                    ImageUrl = book.ImageUrl,
                    Price = book.Price,
                    Title = book.Title,
                    IsFavorite = true,
                    Categories = book.BookCategories.Select(bc => new CategoryViewModel { Name = bc.Category.Name, Id = bc.Category.Id }),
                    Publisher = book.Publisher.Name,
                    PublishedDate = book.PublishedDate
                }),
                Authors = allAuthors.Select(a => new AuthorViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    IsSelected = authors.Any(selectedId => selectedId == a.Id)
                }),
                Categories = allCategories.Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsSelected = categories.Any(selectedId => selectedId == c.Id)
                }),
                Publishers = allPublishers.Select(p => new PublisherViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsSelected = publishers.Any(selectedId => selectedId == p.Id)
                }),
                MinYear = minYear,
                MaxYear = maxYear,
                TotalItemsFound = paginatedBooks.TotalItems,
                CurrentPage = paginatedBooks.CurrentPage,
                TotalPages = paginatedBooks.TotalPages,
                HasNextPage = paginatedBooks.HasNextPage,
                HasPrevPage = paginatedBooks.HasPreviouspage,
            };


            return vm;
        }
    }
}
