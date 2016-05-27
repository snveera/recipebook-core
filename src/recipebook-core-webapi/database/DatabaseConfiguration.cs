using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using recipebook_core_webapi.database.models;
namespace recipebook_core_webapi.database
{
    public static class DatabaseConfiguration
    {
        public static void Migrate(this IApplicationBuilder app)
        {
           var dbContext = app.ApplicationServices.GetService<RecipebookDbContext>();
            
           dbContext.Database.Migrate(); 
        }
        public static void SeedData(this IApplicationBuilder app)
        {
            var dbContext = app.ApplicationServices.GetService<RecipebookDbContext>();
            
            AddOrUpdateRecipe(dbContext,new Recipe{Name = "One",Servings=1,Rating=2,Ingredients="Food",Directions="How to do it"});
        }
        
        private static void AddOrUpdateRecipe(RecipebookDbContext context, Recipe recipe)
        {
            var existingRecipe = context.Recipes
                .FirstOrDefault(r=>r.Name == recipe.Name);
                
            if (existingRecipe == null)
            {
                var newRecipe = new Recipe{
                    Name = recipe.Name,
                    Servings = recipe.Servings,
                    Rating = recipe.Rating,
                    Ingredients = recipe.Ingredients,
                    Directions = recipe.Directions,
                    Source = recipe.Source
                };
                
                context.Recipes.Add(newRecipe);
            }
            else
            {
                existingRecipe.Servings = recipe.Servings;
                existingRecipe.Rating = recipe.Rating;
                existingRecipe.Ingredients = recipe.Ingredients;
                existingRecipe.Directions = recipe.Directions;
                existingRecipe.Source = recipe.Source;
            }
            
            context.SaveChanges();
        }
    }
}