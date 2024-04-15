using FilmsCatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmsCatalogAPI.Data
{
    public class Seed
    {
        public void Initialize(DataContext context)
        {

            if (context.Films.Any() || context.Categories.Any() || context.FilmCategories.Any())
            {
                return;
            }

            var films = Enumerable.Range(1, 10).Select(i => new Film
            {
                Name = $"Film {i}",
                Director = $"Director {i}",
                ReleaseDate = DateTime.Now.AddDays(-i * 10)
            });
            context.Films.AddRange(films);

            var categories = Enumerable.Range(1, 10).Select(i => new Category
            {
                Name = $"Category {i}",
                ParentCategoryId = null
            });
            context.Categories.AddRange(categories);
            context.SaveChanges();
            var filmCategories = new List<FilmCategory>
                {
                    new FilmCategory { FilmId = 1, CategoryId = 1 },
                    new FilmCategory { FilmId = 1, CategoryId = 2 },
                    new FilmCategory { FilmId = 1, CategoryId = 3 },
                    new FilmCategory { FilmId = 2, CategoryId = 1 },
                    new FilmCategory { FilmId = 3, CategoryId = 1 },
                    new FilmCategory { FilmId = 3, CategoryId = 1 },
                };
            context.FilmCategories.AddRange(filmCategories);


            context.SaveChanges();
        }
    }
}
