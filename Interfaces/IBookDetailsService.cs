using BookMania.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Interfaces
{
    public interface IBookDetailsService
    {
        Task<BookDetailsViewModel> GetBookDetailsAsync(int id, int userId);
    }
}
