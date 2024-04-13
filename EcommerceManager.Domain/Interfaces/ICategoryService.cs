using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Interfaces
{
    public interface ICategoryService
    {
        public Task Insert(Category category);
        public Task<List<Category>> GetAll(int limit, int page);
        public Task<Category> GetById(int id);
        public Task Update(Category category);
        public Task Delete(int id);
        public Task<int> GetCount();
    }
}
