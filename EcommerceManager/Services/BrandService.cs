using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using System.Collections.Generic;

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

        public async Task<List<Brand>> GetAll(int limit, int page)
        {
            int skip = 0;

            if(limit < 0 || limit > 1000)
            {
                limit = 10;
            }

            if(page < 0)
            {
                page = 1;
            }

            if (page > 1)
            {
                skip = limit * (page - 1);
            }

            return await _brandDbAccess.GetAll(skip, limit);
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

        public async Task<int> GetCount()
        {
            return await _brandDbAccess.GetCount();
        }
    }
}
