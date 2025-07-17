using DemoNLayerApi.Data.Context;
using DemoNLayerApi.Data.DataDTO;
using DemoNLayerApi.Data.DataDTO.Response;
using DemoNLayerApi.Data.Exceptions;
using DemoNLayerApi.Data.IRepository;
using DemoNLayerApi.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDBContext _dbContext;

        public CategoryRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategory(Category category)
        {
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new NotFoundException("Category not found");

            return category;
        }

        public async Task UpdateCategory(Category category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BooksToCategoryResponse> AddBooksToCategory(BooksToCategory categoryBooks)
        {
            var category = await _dbContext.Categories
                .Include(c => c.Books)
                .Where(c => c.Id == categoryBooks.Id)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                throw new NotFoundException("Category not found");

            }

            var books = await _dbContext.Books.
                Where(b => categoryBooks.BookIds.Contains(b.Id))
                .ToListAsync();

            //Check if filtered books count same as in payload books count
            if (categoryBooks.BookIds.Count != books.Count)
            {
                throw new CustomException("Some of the books doesn't exist");

            }

            foreach (var book in books)
            {
                // If the book is not linked
                if (category.Books.Count == 0 || category.Books.Any(b => b.Id != book.Id))
                {
                    category.Books.Add(book);
                }
            }

            await _dbContext.SaveChangesAsync();

            var categoriesWithBook =  category.Books
                .Select(c => new BooksLinked
                {
                    BookId = c.Id,
                    BookName = c.Title,
                }).ToList();

            var resultSet =  new BooksToCategoryResponse { 
                CategoryName = category.Name, 
                BooksLinked = categoriesWithBook
            };

            return resultSet;

        }
    }
}
