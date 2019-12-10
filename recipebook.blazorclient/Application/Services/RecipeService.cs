using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using recipebook.blazorclient.Application.Models;

namespace recipebook.blazorclient.Application.Services
{
    public class RecipeService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationService _configurationService;

        public RecipeService(HttpClient httpClient, IConfigurationService configurationService)
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

            var data = await _httpClient.GetJsonAsync<ICollection<Recipe>>(uri);

            return data;
        }
    }
}