using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Interfaces
{
    public interface IBrandDbAccess
    {
        public Task AddNewBrand(Brand brand);
        public Task<Brand> GetBrandFromDataBaseById(int id);

        public Task<Brand> GetBrandFromDataBaseByName(string name);
        public Task<Brand> GetBrandFromDataBaseByYearOfFoundation(int year);

        public Task<List<Brand>> GetListBrandsFromDataBase();
        public Task UpdateBrand(Brand brand);
        public Task DeleteBrand(int id);
    }
}
