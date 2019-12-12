using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using recipebook.blazor.core.Models;

namespace recipebook.blazor.core.Services
{
    public interface IUserService
    {
        Task<User> Get();
    }

    public class UserService : IUserService
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