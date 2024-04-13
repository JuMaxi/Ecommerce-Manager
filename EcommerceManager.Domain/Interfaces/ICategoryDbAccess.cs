using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Interfaces
{
    public interface ICategoryDbAccess
    {
        public Task Insert(Category category);
        public Task<Category> GetById(int id);
        public Task<Category> GetByName(string name);
        public Task<Category> GetByDescription(string description);
        public Task<List<Category>> GetAll(int skip, int limit);
        public Task Update(Category category);
        public Task Delete(int id);
        public Task<Category> GetByParentId(int ParentId);
        public Task<int> GetCount();
    }
}
