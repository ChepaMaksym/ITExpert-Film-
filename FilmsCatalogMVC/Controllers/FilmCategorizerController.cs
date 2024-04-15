using FilmsCatalogMVC.Interfaces;
using FilmsCatalogMVC.Models;
using FilmsCatalogMVC.Views.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmsCatalogMVC.Controllers
{
    [Route("filmcategorizer")]
    public class FilmCategorizerController : Controller
    {
        private readonly IFilmCategorizerService _filmCategorizerService;
        private readonly ICategoryService _categoryService;


        public FilmCategorizerController(IFilmCategorizerService filmCategorizerService, ICategoryService categoryService)
        {
            _filmCategorizerService = filmCategorizerService;
            _categoryService = categoryService;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var filmCategories = await _filmCategorizerService.GetFilmCategoriesAsync();
            return View(filmCategories);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var relatedCategories = await _filmCategorizerService.GetFilmCategorizerAsync(id);
            return View(relatedCategories);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FilmCategory category)
        {
            if (ModelState.IsValid)
            {
                await _filmCategorizerService.AddFilmCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet("filmswithcategories")]
        public async Task<ActionResult<IEnumerable<FilmViewModel>>> GetFilmsWithCategories(string directorFilter = null, int? categoryIdFilter = null)
        {
            var filmsWithCategories = await _filmCategorizerService.GetFilmsWithCategoriesAsync(directorFilter, categoryIdFilter);
            return Ok(filmsWithCategories);
        }
        [HttpGet("categorieswithdetails")]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetCategoriesWithDetails()
        {
            var categoriesWithDetails = await _filmCategorizerService.GetCategoriesWithDetailsAsync();
            return Ok(categoriesWithDetails);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var filmCategory = await _filmCategorizerService.GetFilmCategoryByIdAsync(id);
            if (filmCategory == null)
            {
                return NotFound();
            }
            return View(filmCategory);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FilmCategory model)
        {
            var filmCategoryExists = await FilmCategoryExists(id);
            if (!filmCategoryExists)
            {
                return NotFound();
            }

            await _filmCategorizerService.UpdateFilmCategorizerAsync(id, model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filmCategory = await _filmCategorizerService.GetFilmCategoryByIdAsync(id);
            if (filmCategory == null)
            {
                return NotFound();
            }

            return View(filmCategory);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _filmCategorizerService.DeleteFilmCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FilmCategoryExists(int id)
        {
            var filmCategory = await _filmCategorizerService.GetFilmCategoryByIdAsync(id);
            return filmCategory != null;
        }
    }
}
