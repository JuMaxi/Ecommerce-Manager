using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;

namespace EcommerceManager.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandDbAccess _brandDbAccess;
        private readonly IValidateBrand _validateBrand;

        public BrandService(IBrandDbAccess brandDbAccess, IValidateBrand validateBrand)
        {
            _brandDbAccess = brandDbAccess;
            _validateBrand = validateBrand;
        }

        public async Task InsertNewBrand(Brand brand)
        {
            await _validateBrand.Validate(brand);

            await _brandDbAccess.AddNewBrand(brand);
        }

        public async Task<List<Brand>> GetBrandsFromDataBase()
        {
            return await _brandDbAccess.GetListBrandsFromDataBase();
        }

        public async Task UpdateBrand(Brand brand)
        {
            await _validateBrand.Validate(brand);

            Brand toUpdate = await _brandDbAccess.GetBrandFromDataBaseById(brand.Id);

            toUpdate.Name = brand.Name;
            toUpdate.FoundationYear = brand.FoundationYear;

            await _brandDbAccess.UpdateBrand(toUpdate);
        }

        public async Task DeleteBrand(int id)
        {
            await _brandDbAccess.DeleteBrand(id);
        }
    }
}
