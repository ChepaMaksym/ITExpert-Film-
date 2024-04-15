using FilmsCatalogAPI.Models;
using FilmsCatalogAPI.Views;

namespace FilmsCatalogAPI.Interfaces
{
    public interface IFilmCategorizerService
    {
        Task<IEnumerable<CategoryViewModel>> GetCategoriesWithDetailsAsync();
        Task<IEnumerable<FilmViewModel>> GetFilmsWithCategoriesAsync(string directorFilter = null, int? categoryIdFilter = null);
        Task<IEnumerable<CategoryViewModel>> GetRelatedCategoriesAsync(int filmId);
        Task UpdateFilmCategorizerAsync(int id, FilmCategory model);
        Task<IEnumerable<FilmViewModel>> GetFilmsSortedByDateAsync();
        Task<IEnumerable<FilmCategory>> GetFilmCategoriesAsync();
        Task DeleteFilmCategoryAsync(int id);
        Task<FilmCategory> GetFilmCategorizerAsync(int id);
        Task AddFilmCategoryAsync(FilmCategory model);

    }
}
