using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using recipebook.core.Managers;
using recipebook.core.Models;

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

        [FunctionName("api-category-create")]
        public async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "recipe")] HttpRequest req,
            ILogger log)
        {

            var data = await req.ReadAsStringAsync();
            var categoryData = JsonConvert.DeserializeObject<Category>(data);

            var result = _manager.Create(categoryData.Name);

            return new OkObjectResult(result);
        }
    }
}