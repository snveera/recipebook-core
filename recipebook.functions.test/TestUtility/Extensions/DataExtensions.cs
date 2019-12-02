using System;
using recipebook.entityframework;

namespace recipebook.functions.test.TestUtility.Extensions
{
    public static class DataExtensions
    {
        public static string WithRecipe(this TestCompositionRoot root,
            string name,
            string ingredients = null,
            string directions = null,
            int? servings = null,
            string source = null,
            int? rating = null,
            string category = null,
            string id = null)
        {
            var context = root.Get<RecipeBookDbContext>();

            var entity = new entityframework.Models.Recipe
            {
                Id = id ?? Guid.NewGuid().ToString(),
                Name = name,
                Ingredients = ingredients,
                Directions = directions,
                Servings = servings,
                Source = source,
                Rating = rating,
                Category = category
            };
            context.Recipes.Add(entity);

            context.SaveChanges();

            return entity.Id;
        }
    }
}