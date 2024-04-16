using EcommerceManager.Domain.Models;

namespace EcommerceManager.Domain.Interfaces
{
    public interface IProductService
    {
        public Task Insert(Product product);
        public Task<List<Product>> GetAll(int limit, int page);
        public Task Update(Product product);
        public Task Delete(int id);
        public Task<int> GetCount();
    }
}
