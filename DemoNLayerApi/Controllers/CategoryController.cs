using DemoNLayerApi.Business.IServices;
using DemoNLayerApi.Data.DataDTO;
using DemoNLayerApi.Data.DataDTO.Response;
using DemoNLayerApi.DTOs.RequestDTOs;
using DemoNLayerApi.DTOs.ResponseDTOs;
using DemoNLayerApi.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoNLayerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }

        [HttpGet("get-categories")]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategories();

            var categoryResponse = new List<CategoryResponse>();

            foreach (var category in categories)
            {
                categoryResponse.Add(new CategoryResponse
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return Ok(categoryResponse);
        }

        [HttpPost("add-category")]
        public async Task<ActionResult> AddCategory(string name)
        {
            var request = new Category
            {
                Name = name
            };

            await _categoryService.AddCategories(request);
            return Ok("Category added successfully");

        }

        [HttpPut("update-category")]
        public async Task<ActionResult> UpdateCategory(int id, string new_name)
        {
            await _categoryService.UpdateCategory(id, new_name);
            return Ok("Category updated successfully");
        }

        [HttpDelete("delete-category")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            await _categoryService.DeleteCategory(id);
            return Ok("Category deleted successfully");

        }

        [HttpPost("add-books-to-category")]
        public async Task<ActionResult<BooksToCategoryResponse>> AddBooksToCategory([FromBody] BooksToCategory categoryDTO)
        {
           var categoryBooks =  await _categoryService.AddBooksToCategory(categoryDTO);
            return Ok(categoryBooks);
        }
    }
}
