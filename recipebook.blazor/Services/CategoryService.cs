using recipebook.blazor.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace recipebook.blazor.Services
{
    public class CategoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<Category>> GetCategories()
        {
            var client = _httpClientFactory.CreateClient("RecipeApi");
            var response = await client.GetAsync("api/category");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<Category>>();
            return data;
        }
    }
}
