using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using recipebook.core.Managers;
using recipebook.entityframework;

namespace recipebook.functions.Functions
{
    public class DbMigrationFunction
    {
        private readonly RecipeBookDbContext _context;
        private readonly AuthorizationManager _authorizationManager;

        public DbMigrationFunction(RecipeBookDbContext context, AuthorizationManager authorizationManager)
        {
            _context = context;
            _authorizationManager = authorizationManager;
        }

        [FunctionName("api-dbmigration")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "dbmigration")] HttpRequest req,
            ILogger log,
            ClaimsPrincipal user)
        {
            if (!_authorizationManager.CanManageDatabase(user)) return new ForbidResult();

            log.LogInformation("Starting Migration");
            await _context.Database.EnsureCreatedAsync();

            await MigrateDeletionFlag();

            log.LogInformation("Migration Complete");

            return new OkResult();
        }

        private async Task MigrateDeletionFlag()
        {
            var recipes = _context.Recipes.ToList();
            recipes.ForEach(r => r.IsDeleted = false);
            await _context.SaveChangesAsync();
        }
    }
}
