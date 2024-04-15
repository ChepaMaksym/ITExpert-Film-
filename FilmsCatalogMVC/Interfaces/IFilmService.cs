using FilmsCatalogMVC.Models;

namespace FilmsCatalogMVC.Interfaces
{
    public interface IFilmService
    {
        Task<Film> GetFilmByIdAsync(int id);
        Task<IEnumerable<Film>> GetAllFilmsAsync();
        Task<IEnumerable<Film>> GetFilmsSortedByDateAsync();
        Task<IEnumerable<Film>> GetFilmsFilteredByDirectorAsync(string director);

        Task AddFilmAsync(Film film);
        Task UpdateFilmAsync(int id, Film film);
        Task DeleteFilmAsync(int id);
    }
}
