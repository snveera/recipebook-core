using recipebook.blazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace recipebook.blazor.Services
{
    public class RecipeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RecipeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Recipe>> Search(string searchText, string category)
        {
            var client = _httpClientFactory.CreateClient("RecipeApi");
            var response = await client.GetAsync($"api/recipe?criteria={searchText}&category={category}");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<Recipe>>();
            return data.Where(r=>r!=null).ToList();
        }
    }
}
