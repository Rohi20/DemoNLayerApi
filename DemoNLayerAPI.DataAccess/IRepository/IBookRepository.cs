using DemoNLayerApi.Models;
using DemoNLayerApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.IRepository
{
    public interface IBookRepository
    {
        Task AddBooks(Book book);
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetBooksByTitle(string title);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
    }
}
