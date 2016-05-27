using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using recipebook_core_webapi.models;

namespace recipebook_core_webapi.Controllers
{
    [Route("api/[controller]")]
    public class RecipeController : Controller
    {
        private List<RecipeApiModel> _recipes = new List<RecipeApiModel>();
        
        public RecipeController()
        {
            _recipes.Add(new RecipeApiModel{RecipeId=1,Name="one"});
            _recipes.Add(new RecipeApiModel{RecipeId=2,Name="two"});
        }
        
       [HttpGet()]
        public IEnumerable<RecipeApiModel> Get()
        {
            return _recipes;
        }
        
        [HttpGet("{id}")]
        public RecipeApiModel Get(int id)
        {
            return _recipes
                .FirstOrDefault(r=>r.RecipeId == id);
        }
        
        [HttpPost]
        public void Post([FromBody]RecipeApiModel value)
        {
            _recipes.Add(value);
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]RecipeApiModel value)
        {
            var existingRecipe = _recipes
            .FirstOrDefault(r=>r.RecipeId == id);
            
            if(existingRecipe != null)
            {
                existingRecipe.Name = value.Name;
                existingRecipe.Ingredients = value.Ingredients;
            }
        }
        
        [HttpDeleteAttribute("{id}")]
        public void Delete(int id)
        {
             var existingRecipe = _recipes
            .FirstOrDefault(r=>r.RecipeId == id);
            
            if(existingRecipe != null)
            {
                _recipes.Remove(existingRecipe);
            }
        }
        
        
        
        
    }
}