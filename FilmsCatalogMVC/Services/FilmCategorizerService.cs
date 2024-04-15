using FilmsCatalogMVC.Interfaces;
using FilmsCatalogMVC.Models;
using System.Text;
using Newtonsoft.Json;
using FilmsCatalogMVC.Views.Models;

namespace FilmsCatalogMVC.Services
{
    public class FilmCategorizerService : IFilmCategorizerService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FilmCategorizerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesWithDetailsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("filmcategorizerapi");

            var response = await httpClient.GetAsync("/api/filmcategorizer/CategoriesWithDetails");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(content);
        }


        public async Task<IEnumerable<FilmViewModel>> GetFilmsWithCategoriesAsync(string directorFilter = null, int? categoryIdFilter = null)
        {
            var httpClient = _httpClientFactory.CreateClient("filmcategorizerapi");

            var url = "/api/filmcategorizer/FilmsWithCategories?";
            if (!string.IsNullOrEmpty(directorFilter))
                url += $"director={directorFilter}&";
            if (categoryIdFilter.HasValue)
                url += $"categoryId={categoryIdFilter}&";
            url = url.TrimEnd('&');

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<FilmViewModel>>(content);
        }

        public async Task<FilmCategory> GetFilmCategorizerAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("filmcategorizerapi");

            var response = await httpClient.GetAsync($"/api/filmcategorizer/GetFilmCategorizer/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FilmCategory>(content);
        }


        public async Task UpdateFilmCategorizerAsync(int id, FilmCategory model)
        {
            var httpClient = _httpClientFactory.CreateClient("filmcategorizerapi");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"/api/filmcategorizer/UpdateFilmCategorizer/{id}", jsonContent);

            response.EnsureSuccessStatusCode();
        }
        public async Task<FilmCategory> GetFilmCategoryByIdAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("filmcategorizerapi");

            var response = await httpClient.GetAsync($"/api/filmcategorizer/GetFilmCategorizer/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FilmCategory>(content);
        }
        public async Task AddFilmCategoryAsync(FilmCategory model)
        {
            var httpClient = _httpClientFactory.CreateClient("filmcategorizerapi");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/filmcategorizer/AddFilmCategory", jsonContent);

            response.EnsureSuccessStatusCode();
        }
        public async Task<IEnumerable<FilmCategory>> GetFilmCategoriesAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("filmcategorizerapi");

            var response = await httpClient.GetAsync("/api/filmcategorizer"); // TODO: Is it need here?
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<FilmCategory>>(content);
        }
        public async Task DeleteFilmCategoryAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("filmcategorizerapi");

            var response = await httpClient.DeleteAsync($"/api/filmcategorizer/DeleteFilmCategory/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
