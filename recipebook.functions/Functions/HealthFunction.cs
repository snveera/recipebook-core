
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using recipebook.core.Managers;

namespace recipebook.functions.Functions
{
    public class HealthFunction
    {
        private readonly HealthManager _manager;

        public HealthFunction(HealthManager manager)
        {
            _manager = manager;
        }

        [FunctionName("api-health")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req,
            ILogger log,
            ClaimsPrincipal user)
        {
            var result = _manager.Get(this.GetType().Assembly);

            return new OkObjectResult(result);
        }
    }
}
