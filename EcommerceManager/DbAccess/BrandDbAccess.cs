using EcommerceManager.Db;
using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using Microsoft.EntityFrameworkCore;

namespace EcommerceManager.DbAccess
{
    public class BrandDbAccess : IBrandDbAccess
    {
        private readonly EcommerceManagerDbContext _dbContext;

        public BrandDbAccess(EcommerceManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddNewBrand(Brand brand)
        {
            await _dbContext.AddAsync(brand);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Brand> GetBrandFromDataBaseById(int id)
        {
            return await _dbContext.Brands.Where(b => b.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Brand> GetBrandFromDataBaseByName(string name)
        {
            return await _dbContext.Brands.Where(b => b.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Brand> GetBrandFromDataBaseByYearOfFoundation(int year)
        {
            return await _dbContext.Brands.Where(b => b.FoundationYear == year).FirstOrDefaultAsync();
        }

        public async Task<List<Brand>> GetListBrandsFromDataBase()
        {
            return await _dbContext.Brands.ToListAsync();
        }

        public async Task UpdateBrand(Brand brand)
        {
            _dbContext.Brands.Update(brand);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBrand(int id)
        {
            _dbContext.Brands.Remove(await GetBrandFromDataBaseById(id));
            await _dbContext.SaveChangesAsync();
        }
    }
}
