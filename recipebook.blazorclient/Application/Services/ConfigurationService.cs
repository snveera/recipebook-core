namespace recipebook.blazorclient.Application.Services
{
    public interface IConfigurationService
    {
        string UserApiUrl();
        string CategoryApiUrl();
        string RecipeApiUrl();
    }

    public class ConfigurationService : IConfigurationService
    {
        public string UserApiUrl()
        {
            return @"https://recipebook-functions.azurewebsites.net/api/user?code=vSvDxrFqS0WfpGGkpDBo52EoSZUDRzXKLIMpK5l5cO2MIwCA80ZWfg==";
        }
        public string CategoryApiUrl()
        {
            return @"https://recipebook-functions.azurewebsites.net/api/category?code=vSvDxrFqS0WfpGGkpDBo52EoSZUDRzXKLIMpK5l5cO2MIwCA80ZWfg==";
        }
        public string RecipeApiUrl()
        {
            return @"https://recipebook-functions.azurewebsites.net/api/recipe?code=vSvDxrFqS0WfpGGkpDBo52EoSZUDRzXKLIMpK5l5cO2MIwCA80ZWfg==";
        }
    }
}