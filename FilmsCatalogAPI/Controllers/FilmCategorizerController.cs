using FilmsCatalogAPI.Interfaces;
using FilmsCatalogAPI.Models;
using FilmsCatalogAPI.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmsCatalogAPI.Controllers
{
    [Route("api/filmcategorizer")]
    [ApiController]
    public class FilmCategorizerController : ControllerBase
    {
        private readonly IFilmCategorizerService _filmCategorizerService;

        public FilmCategorizerController(IFilmCategorizerService filmCategorizerService)
        {
            _filmCategorizerService = filmCategorizerService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var filmCategories = await _filmCategorizerService.GetFilmCategoriesAsync();
            return Ok(filmCategories);
        }

        [HttpGet("CategoriesWithDetails")]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetCategoriesWithDetails()
        {
            var categoriesWithDetails = await _filmCategorizerService.GetCategoriesWithDetailsAsync();
            return Ok(categoriesWithDetails);
        }

        [HttpGet("FilmsSortedByDate")]
        public async Task<ActionResult<IEnumerable<FilmViewModel>>> GetFilmsSortedByDate()
        {
            var filmsSortedByDate = await _filmCategorizerService.GetFilmsSortedByDateAsync();
            return Ok(filmsSortedByDate);
        }
        [HttpPost("AddFilmCategory")]
        public async Task<IActionResult> AddFilmCategory([FromBody] FilmCategory model)
        {
            try
            {
                await _filmCategorizerService.AddFilmCategoryAsync(model);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("UpdateFilmCategorizer/{id}")]
        public async Task<IActionResult> UpdateFilmCategorizer(int id, [FromBody] FilmCategory model)
        {
            try
            {
                await _filmCategorizerService.UpdateFilmCategorizerAsync(id, model);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("DeleteFilmCategory/{id}")]
        public async Task<IActionResult> DeleteFilmCategory(int id)
        {
            try
            {
                await _filmCategorizerService.DeleteFilmCategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetFilmCategorizer/{id}")]
        public async Task<ActionResult<FilmCategory>> GetFilmCategorizer(int id)
        {
            try
            {
                var filmCategorizer = await _filmCategorizerService.GetFilmCategorizerAsync(id);
                return Ok(filmCategorizer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("FilmsWithCategories")]
        public async Task<ActionResult<IEnumerable<FilmViewModel>>> GetFilmsWithCategories(string directorFilter = null, int? categoryIdFilter = null)
        {
            var filmsWithCategories = await _filmCategorizerService.GetFilmsWithCategoriesAsync(directorFilter, categoryIdFilter);
            return Ok(filmsWithCategories);
        }

        [HttpGet("RelatedCategories/{filmId}")]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetRelatedCategories(int filmId)
        {
            var relatedCategories = await _filmCategorizerService.GetRelatedCategoriesAsync(filmId);
            return Ok(relatedCategories);
        }
    }
}
