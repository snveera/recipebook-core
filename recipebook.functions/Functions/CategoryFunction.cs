using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using recipebook.core.Managers;

namespace recipebook.functions.Functions
{
    public class CategoryFunction
    {
        private readonly CategoryManager _manager;

        public CategoryFunction(CategoryManager manager)
        {
            _manager = manager;
        }

        [FunctionName("api-category")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "category")] HttpRequest req,
            ILogger log)
        {
            var result = _manager.Get();

            return new OkObjectResult(result);
        }
    }
}