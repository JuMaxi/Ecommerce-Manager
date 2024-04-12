using EcommerceManager.Models.DataBase;
using EcommerceManager.Models.Requests;
using EcommerceManager.Models.Responses;

namespace EcommerceManager.Interfaces
{
    public interface ICategoryMapper
    {
        public Category ConvertFromRequest(CategoryRequest categoryRequest);
        public List<CategoryResponse> ConvertToListResponse(List<Category> list);
        public CategoryResponse ConvertToResponse(Category Category);
    }
}
