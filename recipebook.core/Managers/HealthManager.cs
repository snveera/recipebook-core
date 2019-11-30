using Microsoft.Extensions.Logging;
using recipebook.core.Models;
using recipebook.entityframework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace recipebook.core.Managers
{
    public class HealthManager
    {
        private readonly RecipeBookDbContext _dbContext;
        private readonly ILogger<HealthManager> _log;

        public HealthManager(RecipeBookDbContext dbContext, ILogger<HealthManager> log)
        {
            _dbContext = dbContext;
            _log = log;
        }

        public ApplicationHealth Get(Assembly versionedAssembly)
        {
            var dependencyStatus = GetDependencyStatus();
            var overallHealth = !dependencyStatus.Any() || dependencyStatus.Values.All(v => v);

            return new ApplicationHealth
            {
                Version = GetApplicationVersion(versionedAssembly),
                CurrentDateTime = DateTime.Now,
                DependencyStatus = dependencyStatus,
                Status = overallHealth
            };
        }

        public string GetApplicationVersion(Assembly versionedAssembly)
        {
            var version = versionedAssembly?.GetName()?.Version;
            var versionString = version?.ToString();

            return versionString;
        }

        private Dictionary<string, bool> GetDependencyStatus()
        {
            return new Dictionary<string, bool>
            {
                {"database",IsDatabaseHealthy()}
            };
        }

        private bool IsDatabaseHealthy()
        {
            try
            {
                var item = _dbContext.Recipes.FirstOrDefault();
                return true;
            }
            catch (Exception e)
            {
                _log.LogError(e, e.Message);
                return false;
            }

        }
    }
}
