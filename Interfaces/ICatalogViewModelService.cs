using BookMania.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Interfaces
{
    public interface ICatalogViewModelService
    {
        Task<CatalogViewModel> GetFilteredCatalogItemsAsync(FilterResponseViewModel responseFilters, int? userId = default, int pageSize = 30);
    }
}
