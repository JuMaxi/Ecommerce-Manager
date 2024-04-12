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
        public async Task Insert(CategoryRequest categoryRequest)
        {
            Category category = _categoryMapper.ConvertFromRequest(categoryRequest);
            await _categoryService.Insert(category);
        }

        [HttpGet]
        public async Task<List<CategoryResponse>> GetAll()
        {
            List<Category> categories = await _categoryService.GetAll();

            List<CategoryResponse> listCategoriesResponse = _categoryMapper.ConvertToListResponse(categories);

            return listCategoriesResponse;
        }

        [HttpGet("{id}")]
        public async Task<CategoryResponse> GetById([FromRoute] int id)
        {
            return _categoryMapper.ConvertToResponse(await _categoryService.GetById(id));
        }
        

        [HttpPut("{id}")]
        public async Task Update([FromRoute] int id, [FromBody] CategoryRequest categoryRequest)
        {
            Category category = _categoryMapper.ConvertFromRequest(categoryRequest);
            category.Id = id;

            await _categoryService.Update(category);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
           await _categoryService.Delete(id);
        }
    }
}
