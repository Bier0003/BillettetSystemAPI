using BillettetSystemAPI.Interfaces;
using BillettetSystemAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelBilletterSystem;

namespace BillettetSystemAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryRepository;
        private readonly ApplicationDbContext _context;

        public CategoryController(ICategory categoryRepository, ApplicationDbContext context)
        {
            _categoryRepository = categoryRepository;
            _context = context;
        }

        [HttpGet("getAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryRepository.GetAllCategories();
            return Ok(result);
        }

        [HttpGet("getCategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _categoryRepository.GetCategoryById(id);
            return Ok(result);
        }

        [HttpPost("createCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] Category categoryModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (categoryModel.Id_Category == 0)
                return BadRequest("CategoryId is required.");

            var createdCategory = await _categoryRepository.CreateCategory(
                categoryModel.CategoryName
            );

            return Ok(createdCategory);
        }

        [HttpPut("updateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category categoryModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedEvent = await _categoryRepository.UpdateCategory(
                 categoryModel.Id_Category,
                 categoryModel.CategoryName
            );

            return Ok(updatedEvent);
        }

        [HttpDelete("deleteCategory/{id}")]
        public async Task<IActionResult>DeleteCategory(int id)
        {
            var result = await _categoryRepository.DeleteCategory(id);
            return Ok(result);
        }
    }


}
