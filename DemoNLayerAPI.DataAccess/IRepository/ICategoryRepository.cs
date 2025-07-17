using DemoNLayerApi.Data.DataDTO;
using DemoNLayerApi.Data.DataDTO.Response;
using DemoNLayerApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.IRepository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(Category category);
        Task<BooksToCategoryResponse> AddBooksToCategory(BooksToCategory booksCategory);
    }
}
