using DemoNLayerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Business.IServices
{
    public interface IAuthorServices
    {
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(int id, string new_name);
        Task DeleteAuthorAsync(int id);
    }
}
