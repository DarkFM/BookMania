﻿using BookMania.Core.Entities.BookAggregate;
using BookMania.Core.Interfaces;
using BookMania.Interfaces;
using BookMania.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Services
{
    public class CatalogViewModelService : ICatalogViewModelService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IAsyncRepository<Publisher> _publisherRepository;
        private readonly IAsyncRepository<Author> _authorRepository;
        private readonly ILogger<CatalogViewModelService> _logger;

        public CatalogViewModelService(
            ILogger<CatalogViewModelService> logger,
            IBookRepository bookRepository,
            IAsyncRepository<Category> categoryRepository,
            IAsyncRepository<Publisher> publisherRepository,
            IAsyncRepository<Author> authorRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _publisherRepository = publisherRepository;
            _authorRepository = authorRepository;
            _logger = logger;
        }

        public async Task<CatalogViewModel> GetFilteredCatalogItemsAsync(int userId, IEnumerable<int> categories, IEnumerable<int> authors, int pageSize = 0, int pageIndex = 1)
        {
            _logger.LogInformation("GetFilteredCatalogItemsAsync called.");

            if (pageSize < 1) pageSize = 30;

            //var paginatedBooks = await _bookRepository.GetAllBooksWithDataAsync(pageSize, pageIndex);
            var paginatedBooks = await _bookRepository.GetFilteredBooksWithDataAsync(categories, authors, pageSize, pageIndex);
            var allPublishers = await GetAllPublishers();
            var allCategories = await GetAllCategories();
            var allAuthors = await GetAllAuthors();

            var vm = new CatalogViewModel()
            {
                BookItems = paginatedBooks.Select(pb => new BookItemViewModel()
                {
                    BookId = pb.Id,
                    Description = pb.Description,
                    Price = pb.Price,
                    AverageRating = (decimal?)pb.Reviews.Select(r => r.Rating).Average(),
                    Thumbnail = pb.ImageUrl,
                    Title = pb.Title,
                    IsFavorite = pb.Favorites.Any(f => f.UserId == userId && f.BookId == pb.Id),
                    Authors = pb.BookAuthors.Where(ba => ba.BookId == pb.Id).Select(ba => ba.Author.Name)
                }),
                Authors = allAuthors.Select(a => new AuthorViewModel()
                {
                    Id = a.Id,
                    Name = a.Name
                }),
                Categories = allCategories.Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                }),
                Publishers = allPublishers.Select(p => new PublisherViewModel()
                {
                    Id = p.Id,
                    Name = p.Name
                }),
                MinYear = await _bookRepository.GetMinPublishedYear(),
                MaxYear = await _bookRepository.GetMaxPublishedYear(),
                TotalItemsFound = paginatedBooks.TotalItems
            };

            return vm;
        }

        private async Task<IReadOnlyList<Author>> GetAllAuthors()
        {
            return await _authorRepository.ListAllOrderedAsync(x => x.Name);
        }

        private async Task<IReadOnlyList<Category>> GetAllCategories()
        {
            return await _categoryRepository.ListAllOrderedAsync(x => x.Name);
        }

        private async Task<IReadOnlyList<Publisher>> GetAllPublishers()
        {
            return await _publisherRepository.ListAllOrderedAsync(x => x.Name);
        }
    }
}
