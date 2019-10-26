using BookMania.Core.Entities;
using BookMania.Core.Entities.UserAggregate;
using BookMania.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly CatalogContext _dbContext;

        public UserRepository(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Favorite>> GetFavoritesAsync(int userId)
        {
            // https://github.com/aspnet/EntityFrameworkCore/issues/4716
            var user = await _dbContext.Users.AsNoTracking()
                .Where(u => u.Id == userId)
                .Include(u => u.Favorites)
                    .ThenInclude(f => f.Book.Publisher)
                .Include(u => u.Favorites)
                    .ThenInclude(f => f.Book).ThenInclude(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(u => u.Favorites)
                    .ThenInclude(f => f.Book).ThenInclude(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync();

            return user.Favorites.ToList();
        }
    }
}
