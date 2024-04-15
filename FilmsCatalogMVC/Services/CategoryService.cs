using FilmsCatalogMVC.Models;
using FilmsCatalogMVC.Interfaces;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace FilmsCatalogMVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task AddCategoryAsync(Category category)
        {
            var httpClient = _httpClientFactory.CreateClient("categoryapi");

            var jsonContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/category/create", jsonContent);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("categoryapi");

            var response = await httpClient.DeleteAsync($"/api/category/{id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("categoryapi");

            var response = await httpClient.GetAsync("");
            response.EnsureSuccessStatusCode(); 

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Category>>(content);
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("categoryapi");

            var response = await httpClient.GetAsync($"/api/category/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Category>(content);
        }

        public async Task<IEnumerable<Category>> GetRelatedCategoriesAsync(int filmId)
        {
            var httpClient = _httpClientFactory.CreateClient("categoryapi");

            var response = await httpClient.GetAsync($"/api/category/relatedcategories/{filmId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Category>>(content);
        }

        public async Task UpdateCategoryAsync(int id, Category category)
        {
            var httpClient = _httpClientFactory.CreateClient("categoryapi");
            string text = JsonConvert.SerializeObject(category); 
            var jsonContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"/api/category/{id}", jsonContent);

            response.EnsureSuccessStatusCode();
        }
    }
}
