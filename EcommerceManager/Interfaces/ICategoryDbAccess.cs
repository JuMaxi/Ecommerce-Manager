using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Interfaces
{
    public interface ICategoryDbAccess
    {
        public Task AddNewCategory(Category category);
        public Task<Category> GetCategoryFromDbById(int id);
        public Task<Category> GetCategoryFromDbByName(string name);
        public Task<Category> GetCategoryFromDbByDescription(string description);
        public Task<List<Category>> GetListCategoriesFromDb();
        public Task UpdateCategory(Category category);
        public Task DeleteCategory(int id);
        public Task<Category> GetCategoryFromDbByParentId(int ParentId);
    }
}
