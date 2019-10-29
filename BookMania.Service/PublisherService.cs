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
    public class PublisherService : IPublisher
    {
        private readonly CatalogContext _context;

        public PublisherService(CatalogContext catalog)
        {
            _context = catalog;
        }

        public IEnumerable<Publisher> GetAll()
        {
            return _context.Publishers.OrderBy(p => p.Name);
        }
    }
}
