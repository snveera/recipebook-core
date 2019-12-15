namespace recipebook.blazor.core.Services
{
    public interface IConfigurationService
    {
        string UserApiUrl();
        string CategoryApiUrl();
        string RecipeApiUrl();
        string RecipeGetByIdApiUrl();
        string RecipeCreateApiUrl();
        string RecipeUpdateApiUrl();
    }

    public class ConfigurationService : IConfigurationService
    {
        public string UserApiUrl()
        {
            return @"https://recipebook-functions.azurewebsites.net/api/user?code=vSvDxrFqS0WfpGGkpDBo52EoSZUDRzXKLIMpK5l5cO2MIwCA80ZWfg==";
        }
        public string CategoryApiUrl()
        {
            return @"https://recipebook-functions.azurewebsites.net/api/category?code=RriNNfSX5qAdZXUDaIuv/8TpDeA9bB6exLDIYqgI84fkJBmn65fmvw==";
        }
        public string RecipeApiUrl()
        {
            return @"https://recipebook-functions.azurewebsites.net/api/recipe?code=heKFQ0zlfITfqZpq32kPzbFHCn8VEsrArrKRMsuM1B3b9FV4oqNuWg==";
        }
        public string RecipeGetByIdApiUrl()
        {
            return @"https://recipebook-functions.azurewebsites.net/api/recipe/{id}?code=YpvVQoKNCmrEzj2U8tXIUU6k4LTdSZyTH6MFaFppnw0nzVsyccHNOw==";
        }

        public string RecipeCreateApiUrl()
        {
            return @"https://recipebook-functions.azurewebsites.net/api/recipe?code=Dm30pncsQLiBL3JirF/bfYJ6QhtMDJmcSvqJQPYdRTGfelI1iVnY8Q==";
        }

        public string RecipeUpdateApiUrl()
        {
            return @"https://recipebook-functions.azurewebsites.net/api/recipe?code=yZQUXasCUYeejvRwwFqfiVQtIUwXwkdSNUcmfxGOAIircf48b/f9xQ==";
        }
    }
}