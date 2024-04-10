using EcommerceManager.Db;
using EcommerceManager.Interfaces;
using EcommerceManager.Models.DataBase;
using EcommerceManager.Models.Requests;
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

        public async Task AddNewCategory(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryFromDbById(int id)
        {
            return await _dbContext.Categories.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategoryFromDbByName(string name)
        {
            return await _dbContext.Categories.Where(c => c.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategoryFromDbByDescription(string description)
        {
            return await _dbContext.Categories.Where(c => c.Description.Equals(description)).FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategoryFromDbByParentId(int ParentId)
        {
            return await _dbContext.Categories.Where(p => p.Parent.Id.Equals(ParentId)).FirstOrDefaultAsync();
        }
        public async Task<List<Category>> GetListCategoriesFromDb()
        {
            var allCategories = await _dbContext.Categories.Include(c => c.Parent).ToListAsync();
            return allCategories;
        }

        public async Task UpdateCategory(Category category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            Category category = await GetCategoryFromDbById(id);
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
        
    }
}
