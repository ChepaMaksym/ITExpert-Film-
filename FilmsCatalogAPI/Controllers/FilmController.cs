using FilmsCatalogAPI.Interfaces;
using FilmsCatalogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmsCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms()
        {
            var films = await _filmService.GetAllFilmsAsync();
            return Ok(films);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return film;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Film>> PostFilm(Film film)
        {
            await _filmService.AddFilmAsync(film);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, Film film)
        {
            if (id != film.Id)
            {
                return BadRequest();
            }
            await _filmService.UpdateFilmAsync(id, film);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            await _filmService.DeleteFilmAsync(id);
            return NoContent();
        }
    }
}
