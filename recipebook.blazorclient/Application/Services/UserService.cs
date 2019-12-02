using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using recipebook.blazorclient.Application.Models;

namespace recipebook.blazorclient.Application.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationService _configurationService;

        public UserService(HttpClient httpClient, IConfigurationService configurationService)
        {
            _httpClient = httpClient;
            _configurationService = configurationService;
        }

        public async Task<User> Get()
        {
            var uri = $"{_configurationService.UserApiUrl()}";
            var userData = await _httpClient.GetJsonAsync<User>(uri);

            return userData;
        }
    }
}