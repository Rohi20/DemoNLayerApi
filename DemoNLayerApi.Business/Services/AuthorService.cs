using DemoNLayerApi.Business.IServices;
using DemoNLayerApi.Data.IRepository;
using DemoNLayerApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Business.Services
{
    public class AuthorService : IAuthorServices
    {
        private readonly IAuthorRepository _repository;
        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public Task AddAuthorAsync(Author author)
        {
            return _repository.AddAuthorAsync(author);
        }

        public Task DeleteAuthorAsync(int id)
        {
            return _repository.DeleteAuthorAsync(id);
        }

        public Task<List<Author>> GetAllAuthorsAsync()
        {
            return _repository.GetAllAuthorsAsync();
        }

        public Task<Author> GetAuthorByIdAsync(int id)
        {
            return _repository.GetAuthorByIdAsync(id);
        }

        public Task UpdateAuthorAsync(int id, string new_name)
        {
            return (_repository.UpdateAuthorAsync(id, new_name));
        }
    }
}
