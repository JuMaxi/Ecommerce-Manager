using EcommerceManager.Models.DataBase;
using EcommerceManager.Models.Requests;

namespace EcommerceManager.Interfaces
{
    public interface ICategoryService
    {
        public Task Insert(Category category);
        public Task<List<Category>> GetAll();
        public Task<Category> GetById(int id);
        public Task Update(Category category);
        public Task Delete(int id);
    }
}
