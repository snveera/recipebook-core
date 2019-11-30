using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using recipebook.core.Managers;

namespace recipebook.functions.Functions
{
    public class UserFunction
    {
        private readonly UserManager _manager;

        public UserFunction(UserManager manager)
        {
            _manager = manager;
        }

        [FunctionName("api-user")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "user")] HttpRequest req,
            ILogger log)
        {
            var result = _manager.GetCurrent();

            return new OkObjectResult(result);
        }
    }
}
