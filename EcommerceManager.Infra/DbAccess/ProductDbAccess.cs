using EcommerceManager.Db;
using EcommerceManager.Domain.Interfaces;
using EcommerceManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceManager.Infra.DbAccess
{
    public class ProductDbAccess : IProductDbAccess
    {
        private readonly EcommerceManagerDbContext _dbContext;

        public ProductDbAccess(EcommerceManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Insert(Product product)
        {
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _dbContext.Products.Where(p => p.Id == id)
                .Include(c => c.Category)
                .Include(b => b.Brand)
                .FirstOrDefaultAsync();
        }

        public async Task<Product> GetByName(string name)
        {
            return await _dbContext.Products.Where(p => p.Name == name)
                .Include(c => c.Category)
                .Include(b => b.Brand)
                .FirstOrDefaultAsync();
        }

        public async Task<Product> GetByDescription(string description)
        {
            return await _dbContext.Products.Where(p => p.Description == description)
                .Include(c => c.Category)
                .Include(b => b.Brand)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetByCategory(int categoryId)
        {
            return await _dbContext.Products.Where(p => p.Category.Id == categoryId)
                .Include(b => b.Brand)
                .ToListAsync();
        }

        public async Task<List<Product>> GetByBrand(int brandId)
        {
            return await _dbContext.Products.Where(p => p.Brand.Id == brandId)
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<List<Product>> GetByColour(string colour)
        {
            return await _dbContext.Products.Where(p => p.Colour == colour)
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .ToListAsync();
        }

        public async Task<List<Product>> GetAll(int skip, int limit)
        {
            return await _dbContext.Products.Skip(skip)
                .Take(limit)
                .Include(c => c.Category)
                .Include(b => b.Brand)
                .ToListAsync();
        }
        public async Task Update(Product product)
        {
            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Product product = await GetById(id);
            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetCount()
        {
            return await _dbContext.Products.CountAsync();
        }
     
    }
}
