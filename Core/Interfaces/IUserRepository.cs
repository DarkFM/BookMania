using BookMania.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Favorite>> GetFavoritesAsync(int userId);
    }
}
