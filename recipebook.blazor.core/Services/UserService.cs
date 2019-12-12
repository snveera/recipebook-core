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
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfigurationService _configurationService;

        public UserService(IHttpClientFactory httpClient, IConfigurationService configurationService)
        {
            _httpClient = httpClient;
            _configurationService = configurationService;
        }

        public async Task<User> Get()
        {
            var uri = $"{_configurationService.UserApiUrl()}";

            var client = _httpClient.CreateClient();
            var userData = await client.GetJsonAsync<User>(uri);

            return userData;
        }
    }
}