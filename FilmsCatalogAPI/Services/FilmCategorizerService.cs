using FilmsCatalogAPI.Data;
using FilmsCatalogAPI.Interfaces;
using FilmsCatalogAPI.Models;
using FilmsCatalogAPI.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalogAPI.Services
{
    public class FilmCategorizerService : IFilmCategorizerService
    {
        private readonly DataContext _dbContext;

        public FilmCategorizerService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<FilmCategory>> GetFilmCategoriesAsync()
        {
            return await _dbContext.FilmCategories.ToListAsync();
        }


        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesWithDetailsAsync()
        {
            var categoriesWithDetails = await _dbContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    NumberOfFilms = GetNumberOfFilmsByCategory(c.Id), 
                    NestingLevel = GetNestingLevel(c.Id)
                })
                .ToListAsync();

            return categoriesWithDetails;
        }
        public async Task<IEnumerable<FilmViewModel>> GetFilmsSortedByDateAsync()
        {
            return await _dbContext.Films
                .OrderBy(f => f.ReleaseDate)
                .Select(f => new FilmViewModel
                {
                    Id = f.Id,
                    Name = f.Name,
                    Director = f.Director,
                    ReleaseDate = f.ReleaseDate,
                    Categories = f.FilmCategory.Select(fc => fc.Category.Name)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<FilmViewModel>> GetFilmsWithCategoriesAsync(string directorFilter = null, int? categoryIdFilter = null)
        {
            var query = _dbContext.Films.Include(f => f.FilmCategory).ThenInclude(fc => fc.Category).AsQueryable();

            if (!string.IsNullOrEmpty(directorFilter))
            {
                query = query.Where(f => f.Director.Contains(directorFilter));
            }

            if (categoryIdFilter.HasValue)
            {
                query = query.Where(f => f.FilmCategory.Any(fc => fc.CategoryId == categoryIdFilter));
            }

            return await query
                .OrderBy(f => f.ReleaseDate)
                .Select(f => new FilmViewModel
                {
                    Id = f.Id,
                    Name = f.Name,
                    Director = f.Director,
                    ReleaseDate = f.ReleaseDate,
                    Categories = f.FilmCategory.Select(fc => fc.Category.Name)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetRelatedCategoriesAsync(int filmId)
        {
            var relatedCategories = await _dbContext.FilmCategories
                .Where(fc => fc.FilmId == filmId)
                .Select(fc => new CategoryViewModel
                {
                    Id = fc.Category.Id,
                    Name = fc.Category.Name,
                    NumberOfFilms = GetNumberOfFilmsByCategory(fc.Category.Id),
                    NestingLevel = GetNestingLevel(fc.Category.Id)
                })
                .ToListAsync();

            return relatedCategories;
        }

        private int GetNumberOfFilmsByCategory(int categoryId)
        {
            return _dbContext.FilmCategories
                .Count(fc => fc.CategoryId == categoryId);
        }

        private int GetNestingLevel(int categoryId)
        {
            int nestingLevel = 0;
            var category = _dbContext.Categories.Find(categoryId);

            while (category != null && category.ParentCategoryId.HasValue)
            {
                nestingLevel++;
                category = _dbContext.Categories.Find(category.ParentCategoryId.Value);
            }

            return nestingLevel;
        }

        public async Task UpdateFilmCategorizerAsync(int id, FilmCategory model)
        {
            var filmCategorizer = await _dbContext.FilmCategories.FirstOrDefaultAsync(fc => fc.Id == id);
            if (filmCategorizer == null)
            {
                throw new InvalidOperationException("Film categorizer not found");
            }

            _dbContext.FilmCategories.Remove(filmCategorizer);

            var newFilmCategory = new FilmCategory
            {
                Id = id,
                FilmId = model.FilmId,
                CategoryId = model.CategoryId
            };

            _dbContext.FilmCategories.Add(newFilmCategory);

            await _dbContext.SaveChangesAsync();
        }
        public async Task AddFilmCategoryAsync(FilmCategory model)
        {
            var filmExists = await _dbContext.Films.AnyAsync(f => f.Id == model.FilmId);
            var categoryExists = await _dbContext.Categories.AnyAsync(c => c.Id == model.CategoryId);

            if (!filmExists || !categoryExists)
            {
                throw new InvalidOperationException("Film or Category does not exist");
            }

            var newFilmCategory = new FilmCategory
            {
                FilmId = model.FilmId,
                CategoryId = model.CategoryId
            };

            _dbContext.FilmCategories.Add(newFilmCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFilmCategoryAsync(int id)
        {
            var filmCategory = await _dbContext.FilmCategories.FirstOrDefaultAsync(fc => fc.Id == id);
            if (filmCategory == null)
            {
                throw new InvalidOperationException("Film category not found");
            }

            _dbContext.FilmCategories.Remove(filmCategory);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<FilmCategory> GetFilmCategorizerAsync(int id)
        {
            var filmCategorizer = await _dbContext.FilmCategories.FirstOrDefaultAsync(fc => fc.Id == id);

            return filmCategorizer;
        }
    }
}
