using FilmsCatalogAPI.Data;
using FilmsCatalogAPI.Interfaces;
using FilmsCatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmsCatalogAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dbContext;

        public CategoryService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCategoryAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetRelatedCategoriesAsync(int filmId)
        {
            var categories = await _dbContext.FilmCategories
                .Where(fc => fc.FilmId == filmId)
                .Select(fc => fc.Category)
                .ToListAsync();
            return categories;
        }

        public async Task UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = await _dbContext.Categories.FindAsync(id);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.ParentCategoryId = category.ParentCategoryId;
                _dbContext.Categories.Update(existingCategory);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
