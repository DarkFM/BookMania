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
    public class AuthorService : IAuthor
    {
        private readonly CatalogContext _context;

        public AuthorService(CatalogContext catalog)
        {
            _context = catalog;
        }

        public IEnumerable<Author> GetAll()
        {
            return _context.Authors.OrderBy(a => a.Name);
        }
    }
}
