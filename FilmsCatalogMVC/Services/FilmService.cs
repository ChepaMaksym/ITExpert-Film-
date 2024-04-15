using FilmsCatalogMVC.Models;
using FilmsCatalogMVC.Interfaces;
using System.Text;
using Newtonsoft.Json;

namespace FilmsCatalogMVC.Services
{
    public class FilmService : IFilmService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FilmService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task AddFilmAsync(Film film)
        {
            var httpClient = _httpClientFactory.CreateClient("filmapi");

            var jsonContent = new StringContent(JsonConvert.SerializeObject(film), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/film/create", jsonContent);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteFilmAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("filmapi");

            var response = await httpClient.DeleteAsync($"/api/film/{id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Film>> GetAllFilmsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("filmapi");

            var response = await httpClient.GetAsync("/api/film");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Film>>(content);
        }

        public async Task<Film> GetFilmByIdAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("filmapi");

            var response = await httpClient.GetAsync($"/api/film/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Film>(content);
        }

        public async Task<IEnumerable<Film>> GetFilmsFilteredByDirectorAsync(string director)
        {
            var httpClient = _httpClientFactory.CreateClient("filmapi");

            var response = await httpClient.GetAsync($"/api/film?director={director}"); // TODO: directro
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Film>>(content);
        }

        public async Task<IEnumerable<Film>> GetFilmsSortedByDateAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("filmapi");

            var response = await httpClient.GetAsync("/api/film/sortedbydate");// TODO: sortedbydate
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Film>>(content);
        }

        public async Task UpdateFilmAsync(int id, Film film)
        {
            var httpClient = _httpClientFactory.CreateClient("filmapi");

            var jsonContent = new StringContent(JsonConvert.SerializeObject(film), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"/api/film/{id}", jsonContent);

            response.EnsureSuccessStatusCode();
        }
    }
}
