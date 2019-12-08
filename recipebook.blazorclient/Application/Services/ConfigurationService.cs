﻿namespace recipebook.blazorclient.Application.Services
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
            return @"https://recipebook-functions.azurewebsites.net/api/category?code=RriNNfSX5qAdZXUDaIuv/8TpDeA9bB6exLDIYqgI84fkJBmn65fmvw==";
        }
        public string RecipeApiUrl()
        {
            return @"https://recipebook-functions.azurewebsites.net/api/recipe?code=heKFQ0zlfITfqZpq32kPzbFHCn8VEsrArrKRMsuM1B3b9FV4oqNuWg==";
        }
    }
}