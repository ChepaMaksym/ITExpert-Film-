using FilmsCatalogMVC.Interfaces;
using FilmsCatalogMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmsCatalogMVC.Controllers
{
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var films = await _filmService.GetAllFilmsAsync();
            return View(films);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Film film)
        {
            if (ModelState.IsValid)
            {
                await _filmService.AddFilmAsync(film);
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Film film)
        {
            if (id != film.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _filmService.UpdateFilmAsync(id, film);
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _filmService.DeleteFilmAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
