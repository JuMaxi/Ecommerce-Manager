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

        public async Task Insert(Brand brand)
        {
            await _validateBrand.Validate(brand);

            await _brandDbAccess.Insert(brand);
        }

        public async Task<List<Brand>> GetAll()
        {
            return await _brandDbAccess.GetAll();
        }

        public async Task<Brand> GetById(int id)
        {
            return await _brandDbAccess.GetById(id);
        }
        public async Task Update(Brand brand)
        {
            await _validateBrand.Validate(brand);

            Brand toUpdate = await _brandDbAccess.GetById(brand.Id);

            toUpdate.Name = brand.Name;
            toUpdate.FoundationYear = brand.FoundationYear;

            await _brandDbAccess.Update(toUpdate);
        }

        public async Task Delete(int id)
        {
            await _brandDbAccess.Delete(id);
        }
    }
}
