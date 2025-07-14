using DemoNLayerApi.Business.IServices;
using DemoNLayerApi.DTOs.RequestDTOs;
using DemoNLayerApi.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoNLayerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorServices _authorServices;

        public AuthorController(IAuthorServices authorServices)
        {
            _authorServices = authorServices;

        }

        [HttpGet("get-authors")]
        [Authorize]
        public async Task<ActionResult<List<AuthorDTO>>> GetAuthors()
        {
            var authors = await _authorServices.GetAllAuthorsAsync();
            var dto = authors.Select(a => new Author
            {
                Id = a.Id,
                Name = a.Name,
                Books = a.Books.Select(b => new Book
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Categories = b.Categories.Select(c => new Category
                    {
                        Id = c.Id,
                        Name = c.Name,
                    }).ToList()

                }).ToList()

            });
            return Ok(dto);
        }

        [HttpPost("create-author")]
        public async Task<ActionResult> AddAuthor(string name)
        {
            var request = new Author
            {
                Name = name
            };

            await _authorServices.AddAuthorAsync(request);
            return Ok("Author created successfully");

        }

        [HttpPut("update-author")]
        public async Task<ActionResult> UpdateAuthor(int id, string new_name)
        {
            await _authorServices.UpdateAuthorAsync(id, new_name);
            return Ok("Author updated successfully");
        }

        [HttpDelete("delete-author")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            await _authorServices.DeleteAuthorAsync(id);
            return Ok("Author deleted successfully");

        }

        [HttpGet("get-author-by-id")]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            var author = await _authorServices.GetAuthorByIdAsync(id);


            return Ok(author);
        }

    }
}
