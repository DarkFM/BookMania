using BookMania.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookMania.Data.Interfaces
{
    public interface IAuthor
    {
        IEnumerable<Author> GetAll();
    }
}
