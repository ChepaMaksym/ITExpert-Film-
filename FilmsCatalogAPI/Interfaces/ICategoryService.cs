using FilmsCatalogAPI.Models;

namespace FilmsCatalogAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Category>> GetRelatedCategoriesAsync(int filmId);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(int id, Category category);
        Task DeleteCategoryAsync(int id);
        Task<Category> GetCategoryByIdAsync(int id);
    }
}
