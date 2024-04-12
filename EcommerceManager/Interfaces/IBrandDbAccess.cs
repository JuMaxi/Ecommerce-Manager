using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Interfaces
{
    public interface IBrandDbAccess
    {
        public Task Insert(Brand brand);
        public Task<Brand> GetById(int id);

        public Task<Brand> GetByName(string name);
        public Task<Brand> GetByFoundationYear(int year);

        public Task<List<Brand>> GetAll();
        public Task Update(Brand brand);
        public Task Delete(int id);
    }
}
