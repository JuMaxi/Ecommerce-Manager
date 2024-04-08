using EcommerceManager.Models.DataBase;
using EcommerceManager.Models.Requests;
using EcommerceManager.Models.Responses;

namespace EcommerceManager.Interfaces
{
    public interface ICategoryMapper
    {
        public Category ConvertCategoryRequestToCategory(CategoryRequest categoryRequest);
        public List<CategoryResponse> ConvertCategoryToCategoryResponse(List<Category> list);
    }
}
