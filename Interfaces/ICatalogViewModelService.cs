using BookMania.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Interfaces
{
    public interface ICatalogViewModelService
    {
        Task<CatalogViewModel> GetFilteredCatalogItemsAsync(int userId, IEnumerable<int> categories, IEnumerable<int> authors, int pageSize = default, int pageIndex = 1);
    }
}
