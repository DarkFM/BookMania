using BookMania.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookMania.Data.Interfaces
{
    public interface IApplicationUser
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(int userId);
        Task AddUser(ApplicationUser newUser);
        Task UpdateUser(ApplicationUser user);
        Task DeleteAsync(ApplicationUser user);
    }
}
