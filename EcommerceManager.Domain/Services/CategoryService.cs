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

        public async Task Insert(Category category)
        {
            await _validateCategory.Validate(category);

            if (category.Parent != null)
            {
                category.Parent = await _categoryDbAccess.GetById(category.Parent.Id);
            }

            await _categoryDbAccess.Insert(category);
        }

        public async Task<List<Category>> GetAll(int limit, int page)
        {
            int skip = 0;

            if (limit < 0 || limit > 1000)
            {
                limit = 10;
            }

            if (page < 0)
            {
                page = 1;
            }

            if (page > 1)
            {
                skip = limit * (page - 1);
            }
            return await _categoryDbAccess.GetAll(skip, limit);
        }

        public async Task<Category> GetById(int id)
        {
            return await _categoryDbAccess.GetById(id);
        }
        public async Task Update(Category category)
        {
            await _validateCategory.Validate(category);

            Category toUpdate = await _categoryDbAccess.GetById(category.Id);

            toUpdate.Name = category.Name;
            toUpdate.Description = category.Description;
            toUpdate.Image = category.Image;

            if (category.Parent != null)
            {
                Category parent = new();
                parent = await _categoryDbAccess.GetById(category.Parent.Id);
                toUpdate.Parent = parent;
            }
            else
            {
                if (toUpdate.Parent != null)
                {
                    toUpdate.Parent = null;
                }
            }

            await _categoryDbAccess.Update(toUpdate);
        }

        public async Task Delete(int id)
        {
            if (await _categoryDbAccess.GetByParentId(id) != null)
            {
                throw new Exception("There are children categories for Id " + id + ". Please, verify the children categories before delete.");
            }

            await _categoryDbAccess.Delete(id);
        }

        public async Task<int> GetCount()
        {
            return await _categoryDbAccess.GetCount();
        }
    }
}
