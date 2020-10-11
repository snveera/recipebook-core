using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using recipebook.entityframework;

namespace recipebook.functions.Functions
{
    public class DbMigrationFunction
    {
        private readonly RecipeBookDbContext _context;

        public DbMigrationFunction(RecipeBookDbContext context)
        {
            _context = context;
        }

        [FunctionName("api-dbmigration")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "dbmigration")] HttpRequest req,
            ILogger log,
            ClaimsPrincipal user)
        {
            log.LogInformation("Starting Migration");
            await _context.Database.EnsureCreatedAsync();
            log.LogInformation("Migration Complete");

            return new OkResult();
        }
    }
}
