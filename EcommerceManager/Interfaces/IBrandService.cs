using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Interfaces
{
    public interface IBrandService
    {
        public Task InsertNewBrand(Brand brand);
        public Task<List<Brand>> GetBrandsFromDataBase();
        public Task UpdateBrand(Brand brand);
        public Task DeleteBrand(int id);
    }
}
