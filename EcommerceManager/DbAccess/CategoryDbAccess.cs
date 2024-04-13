using EcommerceManager.Db;
using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using Microsoft.EntityFrameworkCore;

namespace EcommerceManager.DbAccess
{
    public class CategoryDbAccess : ICategoryDbAccess
    {
        private readonly EcommerceManagerDbContext _dbContext;

        public CategoryDbAccess(EcommerceManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Insert(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _dbContext.Categories.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Category> GetByName(string name)
        {
            return await _dbContext.Categories.Where(c => c.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<Category> GetByDescription(string description)
        {
            return await _dbContext.Categories.Where(c => c.Description.Equals(description)).FirstOrDefaultAsync();
        }

        public async Task<Category> GetByParentId(int ParentId)
        {
            return await _dbContext.Categories.Where(p => p.Parent.Id.Equals(ParentId)).FirstOrDefaultAsync();
        }
        public async Task<List<Category>> GetAll(int skip, int limit)
        {
            return await _dbContext.Categories.Skip(skip)
                .Take(limit)
                .Include(c => c.Parent)
                .ToListAsync();
        }

        public async Task Update(Category category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Category category = await GetById(id);
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task<int> GetCount()
        {
            return await _dbContext.Categories.CountAsync();
        }
    }
}
