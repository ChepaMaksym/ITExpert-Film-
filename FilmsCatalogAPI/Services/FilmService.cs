using FilmsCatalogAPI.Data;
using FilmsCatalogAPI.Interfaces;
using FilmsCatalogAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalogAPI.Services
{
    public class FilmService : IFilmService
    {
        private readonly DataContext _dbContext;

        public FilmService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddFilmAsync(Film film)
        {
            _dbContext.Films.Add(film);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFilmAsync(int id)
        {
            var film = await _dbContext.Films.FindAsync(id);
            if (film != null)
            {
                _dbContext.Films.Remove(film);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Film>> GetAllFilmsAsync()
        {
            return await _dbContext.Films.ToListAsync();
        }

        public async Task<Film> GetFilmByIdAsync(int id)
        {
            return await _dbContext.Films.FindAsync(id);
        }

        public async Task<IEnumerable<Film>> GetFilmsFilteredByDirectorAsync(string director)
        {
            return await _dbContext.Films.Where(f => f.Director == director).ToListAsync();
        }

        public async Task<IEnumerable<Film>> GetFilmsSortedByDateAsync()
        {
            return await _dbContext.Films.OrderBy(f => f.ReleaseDate).ToListAsync();
        }

        public async Task UpdateFilmAsync(int id, Film film)
        {
            var existingFilm = await _dbContext.Films.FindAsync(id);
            if (existingFilm != null)
            {
                existingFilm.Name = film.Name;
                existingFilm.Director = film.Director;
                existingFilm.ReleaseDate = film.ReleaseDate;
                _dbContext.Films.Update(existingFilm); // TODO: Check ICollection<FilmCategory>
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
