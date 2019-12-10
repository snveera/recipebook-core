
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
    public class RecipeFunction
    {
        private readonly RecipeManager _manager;

        public RecipeFunction(RecipeManager manager)
        {
            _manager = manager;
        }

        [FunctionName("api-recipe-get-all")]
        public async Task<IActionResult> GetAll(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "recipe")] HttpRequest req,
            ILogger log)
        {
            var searchCriteria = req.Query["criteria"];
            var searchCategory = req.Query["category"];
            var result = await _manager.Search(searchCriteria,searchCategory);

            return new OkObjectResult(result);
        }

        [FunctionName("api-recipe-get-byid")]
        public async Task<IActionResult> GetItem(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "recipe/{id}")] HttpRequest req,
            string id,
            ILogger log
            )
        {
            var result = await _manager.GetById(id);

            return new OkObjectResult(result);
        }

        [FunctionName("api-recipe-create")]
        public async Task<IActionResult> Create(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = "recipe")] HttpRequest req,
           ILogger log)
        {

            var data = await req.ReadAsStringAsync();
            var recipeData = JsonConvert.DeserializeObject<Recipe>(data);

            var result = await _manager.Create(recipeData);

            return new OkObjectResult(result);
        }

        [FunctionName("api-recipe-update")]
        public async Task<IActionResult> Update(
          [HttpTrigger(AuthorizationLevel.Function, "put", Route = "recipe")] HttpRequest req,
          ILogger log)
        {

            var data = await req.ReadAsStringAsync();
            var recipeData = JsonConvert.DeserializeObject<Recipe>(data);

            var result = await _manager.Update(recipeData);

            return new OkObjectResult(result);
        }
    }
}
