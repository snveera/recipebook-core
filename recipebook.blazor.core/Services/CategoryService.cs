using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using recipebook.blazor.core.Models;

namespace recipebook.blazor.core.Services
{
    public interface ICategoryService
    {
        Task<ICollection<Category>> Get();
    }

    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfigurationService _configurationService;

        public CategoryService(IHttpClientFactory httpClient, IConfigurationService configurationService)
        {
            _httpClient = httpClient;
            _configurationService = configurationService;
        }

        public async Task<ICollection<Category>> Get()
        {
            var uri = $"{_configurationService.CategoryApiUrl()}";
            var client = _httpClient.CreateClient();
            var data = await client.GetJsonAsync<ICollection<Category>>(uri);

            return data;
        }
    }

    public class CachedCategoryService:ICategoryService
    {
        private readonly CategoryService _categoryService;

        private ICollection<Category> _cachedData = null;

        public CachedCategoryService(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ICollection<Category>> Get()
        {
            return _cachedData ?? (_cachedData = await _categoryService.Get());
        }
    }

    
}