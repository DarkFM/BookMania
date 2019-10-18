using BookMania.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Interfaces
{
    public interface ICatalogViewModelService
    {
        Task<CatalogViewModel> GetFilteredCatalogItemsAsync(int userId, FilterResponseViewModel responseFilters, int pageSize = default);
    }
}
