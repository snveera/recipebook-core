using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using recipebook_core_webapi.models;
using recipebook_core_webapi.database;
using recipebook_core_webapi.database.models;

namespace recipebook_core_webapi.Controllers
{
    [Route("api/[controller]")]
    public class RecipeController : Controller
    {
        private RecipebookDbContext _dbContext;
        
        public RecipeController(RecipebookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
       [HttpGet()]
        public IEnumerable<RecipeApiModel> Get()
        {
            return _dbContext.Recipes.Select(Map);
        }
        
        [HttpGet("{id}")]
        public RecipeApiModel Get(int id)
        {
            var data = _dbContext.Recipes
                .FirstOrDefault(r=>r.RecipeId == id)
                ;
                
            return Map(data);
        }
        
        [HttpPost]
        public void Post([FromBody]RecipeApiModel value)
        {
            var data = Map(value);
            
            _dbContext.Recipes.Add(data);
            _dbContext.SaveChanges();
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]RecipeApiModel value)
        {
            var existingRecipe = _dbContext.Recipes
                .FirstOrDefault(r=>r.RecipeId == id);
            
            if(existingRecipe != null)
            {
                existingRecipe.Name = value.Name;
                existingRecipe.Ingredients = value.Ingredients;
                
                _dbContext.SaveChanges();
            }
        }
        
        [HttpDeleteAttribute("{id}")]
        public void Delete(int id)
        {
             var existingRecipe = _dbContext.Recipes
            .FirstOrDefault(r=>r.RecipeId == id);
            
            if(existingRecipe != null)
            {
                _dbContext.Recipes.Remove(existingRecipe);
                _dbContext.SaveChanges();
            }
        }

        public RecipeApiModel Map(Recipe recipe)
        {
            return new RecipeApiModel{
                RecipeId = recipe.RecipeId,
                Name = recipe.Name,
                Servings = recipe.Servings,
                Rating = recipe.Rating,
                Ingredients = recipe.Ingredients,
                Directions = recipe.Directions,
                Source = recipe.Source
            };
        }
        
        public Recipe Map(RecipeApiModel recipe)
        {
            return new Recipe{
                RecipeId = recipe.RecipeId,
                Name = recipe.Name,
                Servings = recipe.Servings,
                Rating = recipe.Rating,
                Ingredients = recipe.Ingredients,
                Directions = recipe.Directions,
                Source = recipe.Source
            };
        }
    }
}