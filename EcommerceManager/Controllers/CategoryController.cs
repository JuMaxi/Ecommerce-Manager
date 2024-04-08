using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using EcommerceManager.Models.Requests;
using EcommerceManager.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceManager.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CategoryController : ControllerBase
    {
        readonly ICategoryService _categoryService;
        readonly ICategoryMapper _categoryMapper;

        public CategoryController(ICategoryService categoryService, ICategoryMapper categoryMapper)
        {
            _categoryService = categoryService;
            _categoryMapper = categoryMapper;
        }

        [HttpPost]
        public async Task AddNewCategory(CategoryRequest categoryRequest)
        {
            Category category = _categoryMapper.ConvertCategoryRequestToCategory(categoryRequest);
            await _categoryService.InsertNewCategory(category);
        }

        [HttpGet]
        public async Task<List<CategoryResponse>> GetCategoriesFromDb()
        {
            List<Category> categories = await _categoryService.GetAllCategoriesFromDb();

            List<CategoryResponse> listCategoriesResponse = _categoryMapper.ConvertCategoryToCategoryResponse(categories);

            return listCategoriesResponse;
        }

        [HttpPut("{id}")]
        public async Task UpdateCategory([FromRoute] int id, [FromBody] CategoryRequest categoryRequest)
        {
            Category category = _categoryMapper.ConvertCategoryRequestToCategory(categoryRequest);
            category.Id = id;

            await _categoryService.UpdateCategory(category);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCategory([FromRoute] int id)
        {
           await _categoryService.DeleteCategory(id);
        }
    }
}
