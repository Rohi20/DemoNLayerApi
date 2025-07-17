using DemoNLayerApi.Business.IServices;
using DemoNLayerApi.Data.DataDTO;
using DemoNLayerApi.Data.DataDTO.Response;
using DemoNLayerApi.Data.IRepository;
using DemoNLayerApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddCategory(Category category)
        {
            await _categoryRepository.AddCategory(category);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            await _categoryRepository.DeleteCategory(category);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategories();
            return categories;
        }

        public async Task UpdateCategory(int id, string new_category)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            category.Name = new_category;
            await _categoryRepository.UpdateCategory(category);
        }

        public async Task AddCategories(Category category)
        {
            await _categoryRepository.AddCategory(category);
        }

        public async Task<BooksToCategoryResponse> AddBooksToCategory(BooksToCategory booksCategory)
        {
            return await _categoryRepository.AddBooksToCategory(booksCategory);
        }
    }
}
