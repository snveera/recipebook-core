using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using recipebook.blazor.core.Models;

namespace recipebook.blazor.core.Services
{
    public interface IRecipeService
    {
        Task<ICollection<Recipe>> Get(string criteria, string category);
        Task<Recipe> GetById(string id);
    }

    public class RecipeService : IRecipeService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfigurationService _configurationService;

        public RecipeService(IHttpClientFactory httpClient, IConfigurationService configurationService)
        {
            _httpClient = httpClient;
            _configurationService = configurationService;
        }

        public async Task<ICollection<Recipe>> Get(string criteria, string category)
        {
            var uri = $"{_configurationService.RecipeApiUrl()}";
            if (!string.IsNullOrWhiteSpace(criteria))
            {
                uri += $"&criteria={criteria}";
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                uri += $"&category={category}";
            }

            var client = _httpClient.CreateClient();
            var data = await client.GetJsonAsync<ICollection<Recipe>>(uri);

            return data;
        }

        public async Task<Recipe> GetById(string id)
        {
            var uri = _configurationService.RecipeGetByIdApiUrl().Replace("{id}",id);

            var client = _httpClient.CreateClient();
            var data = await client.GetJsonAsync<Recipe>(uri);

            return data;
        }
    }

}