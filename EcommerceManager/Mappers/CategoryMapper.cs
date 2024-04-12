using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using EcommerceManager.Models.Requests;
using EcommerceManager.Models.Responses;

namespace EcommerceManager.Mappers
{
    public class CategoryMapper : ICategoryMapper
    {
        public Category ConvertFromRequest(CategoryRequest categoryRequest)
        {
            Category category = new Category()
            {
                Name = categoryRequest.Name,
                Description = categoryRequest.Description,
                Image = categoryRequest.Image,
            };

            if(categoryRequest.ParentId != 0)
            {
                category.Parent = new()
                {
                    Id = categoryRequest.ParentId
                };
            }

            return category;
        }
        public List<CategoryResponse> ConvertToListResponse(List<Category> list)
        {
            List<CategoryResponse> listCategoriesResponse = new();
            foreach(Category c in list)
            {
                Category parent = new();

                if(c.Parent is not null)
                {
                    parent.Name = c.Parent.Name;
                    parent.Id = c.Parent.Id;
                }

                CategoryResponse categoryResponse = new()
                {
                    Name = c.Name,
                    Description = c.Description,
                    Image = c.Image,
                    ParentName = parent.Name,
                    ParentId = parent.Id
                };
                listCategoriesResponse.Add(categoryResponse);
            }
            return listCategoriesResponse;
        }
    }
}
