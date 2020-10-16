using recipebook.blazor.Models;
using System;

namespace recipebook.blazor.test.TestUtility.Extensions
{
    public static class DataExtensions
    {
        public static TestCompositionRoot WithCategory(this TestCompositionRoot root, string name)
        {
            root.TestContext.Categories.Add(new Category { Name = name });
            return root;
        }

        public static TestCompositionRoot WithRecipe(this TestCompositionRoot root, 
            string name, 
            string category = "some-category",
            int? servings = null,
            string ingredients = "something to add",
            string directions = "something to do",
            string source = "where I cam from",
            string id = null)
        {
            root.TestContext.Recipes.Add(new Recipe 
            { 
                Id = id ?? Guid.NewGuid().ToString(),
                Name = name,
                Servings = servings,
                Ingredients = ingredients,
                Directions = directions,
                Source = source,
                Category = category
            });
            return root;
        }
    }
}
