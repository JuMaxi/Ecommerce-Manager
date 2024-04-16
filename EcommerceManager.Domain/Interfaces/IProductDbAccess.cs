using EcommerceManager.Domain.Models;

namespace EcommerceManager.Domain.Interfaces
{
    public interface IProductDbAccess
    {
        public Task Insert(Product product);
        public Task<Product> GetById(int id);
        public Task<Product> GetByName(string name);
        public Task<Product> GetByDescription(string description);
        public Task<List<Product>> GetByCategory(int categoryId);
        public Task<List<Product>> GetByBrand(int brandId);
        public Task<List<Product>> GetByColour(string colour);
        public Task<List<Product>> GetAll(int skip, int limit);
        public Task Update(Product product);
        public Task Delete(int id);
        public Task<int> GetCount();
    }
}
