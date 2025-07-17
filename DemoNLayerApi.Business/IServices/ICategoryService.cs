using DemoNLayerApi.Data.DataDTO;
using DemoNLayerApi.Data.DataDTO.Response;
using DemoNLayerApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Business.IServices
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task AddCategories(Category category);
        Task UpdateCategory(int id, string new_category);
        Task DeleteCategory(int id);
        Task<BooksToCategoryResponse> AddBooksToCategory(BooksToCategory booksCategory);
    }
}
