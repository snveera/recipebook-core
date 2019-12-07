using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using recipebook.blazorclient.Application.Models;

namespace recipebook.blazorclient.Application.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationService _configurationService;

        public CategoryService(HttpClient httpClient, IConfigurationService configurationService)
        {
            _httpClient = httpClient;
            _configurationService = configurationService;
        }

        public async Task<ICollection<Category>> Get()
        {
            var uri = $"{_configurationService.CategoryApiUrl()}";
            var data = await _httpClient.GetJsonAsync<ICollection<Category>>(uri);

            return data;
        }
    }
}