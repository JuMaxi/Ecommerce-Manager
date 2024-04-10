using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDbAccess _categoryDbAccess;
        private readonly IValidateCategory _validateCategory;

        public CategoryService(ICategoryDbAccess categoryDbAccess, IValidateCategory validateCategory)
        {
            _categoryDbAccess = categoryDbAccess;
            _validateCategory = validateCategory;
        }

        public async Task InsertNewCategory(Category category)
        {
            await _validateCategory.Validate(category);

            if (category.Parent != null)
            {
                category.Parent = await _categoryDbAccess.GetCategoryFromDbById(category.Parent.Id);
            }

            await _categoryDbAccess.AddNewCategory(category);
        }

        public async Task<List<Category>> GetAllCategoriesFromDb()
        {
            List<Category> categories = await _categoryDbAccess.GetListCategoriesFromDb();
            return categories;
        }

        public async Task UpdateCategory(Category category)
        {
            await _validateCategory.Validate(category);

            Category toUpdate = await _categoryDbAccess.GetCategoryFromDbById(category.Id);

            toUpdate.Name = category.Name;
            toUpdate.Description = category.Description;
            toUpdate.Image = category.Image;

            if (category.Parent != null)
            {
                Category parent = new();
                parent = await _categoryDbAccess.GetCategoryFromDbById(category.Parent.Id);
                toUpdate.Parent = parent;
            }
            else
            {
                if (toUpdate.Parent != null)
                {
                    toUpdate.Parent = null;
                }
            }

            await _categoryDbAccess.UpdateCategory(toUpdate);
        }

        public async Task DeleteCategory(int id)
        {
            if (await _categoryDbAccess.GetCategoryFromDbByParentId(id) != null)
            {
                throw new Exception("There are children categories for Id " + id + ". Please, verify the children categories before delete.");
            }

            await _categoryDbAccess.DeleteCategory(id);
        }
    }
}
