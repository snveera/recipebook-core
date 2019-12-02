namespace recipebook.blazorclient.Application.Services
{
    public interface IConfigurationService
    {
        string UserApiUrl();
    }

    public class ConfigurationService : IConfigurationService
    {
        public string UserApiUrl()
        {
            return "http://localhost:7071/api/user";
        }
    }
}