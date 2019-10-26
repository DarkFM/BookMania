using BookMania.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Interfaces
{
    public interface IUserService
    {
        Task<CatalogViewModel> GetFavoritesAsync(FilterResponseViewModel responseFilters, int userId, int pageSize = 30);
    }
}
