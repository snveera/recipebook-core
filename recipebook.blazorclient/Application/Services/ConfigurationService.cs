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
            return @"https://recipebook-functions.azurewebsites.net/api/user?code=vSvDxrFqS0WfpGGkpDBo52EoSZUDRzXKLIMpK5l5cO2MIwCA80ZWfg==";
        }
    }
}