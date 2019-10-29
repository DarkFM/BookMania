using BookMania.Data;
using BookMania.Data.Interfaces;
using BookMania.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMania.Service
{
    public class CategoryService : ICategory
    {
        private readonly CatalogContext _context;

        public CategoryService(CatalogContext catalog)
        {
            _context = catalog;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.OrderBy(c => c.Name);
        }
    }
}
