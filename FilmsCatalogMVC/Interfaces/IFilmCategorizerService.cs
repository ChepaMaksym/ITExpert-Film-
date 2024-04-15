using FilmsCatalogMVC.Models;
using FilmsCatalogMVC.Views.Models;

namespace FilmsCatalogMVC.Interfaces
{
    public interface IFilmCategorizerService
    {
        Task<IEnumerable<CategoryViewModel>> GetCategoriesWithDetailsAsync();
        Task<IEnumerable<FilmViewModel>> GetFilmsWithCategoriesAsync(string directorFilter = null, int? categoryIdFilter = null);
        Task<FilmCategory> GetFilmCategorizerAsync(int id);
        Task UpdateFilmCategorizerAsync(int id, FilmCategory model);
        Task<IEnumerable<FilmCategory>> GetFilmCategoriesAsync();
        Task<FilmCategory> GetFilmCategoryByIdAsync(int id);
        Task DeleteFilmCategoryAsync(int id);
        Task AddFilmCategoryAsync(FilmCategory model);
    }
}
