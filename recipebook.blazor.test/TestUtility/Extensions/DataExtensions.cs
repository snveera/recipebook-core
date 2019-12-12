using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using recipebook.blazorclient.Application.Models;

namespace recipebook.blazor.test.TestUtility.Extensions
{
    public static class DataExtensions
    {
        public static void WithUser(this TestCompositionRoot root, 
            string name,
            ICollection<KeyValuePair<string,string>> claims = null)
        {
            root.Context.CurrentUser.Name = name;
            root.Context.CurrentUser.Claims = claims?
                .Select(c => new UserClaim {Type = c.Key, Value = c.Value})?
                .ToList();
        }

        public static void WithCategory(this TestCompositionRoot root, string name)
        {
            root.Context.CategoriesInApi.Add(new Category {Name = name});
        }

        public static void WithRecipe(this TestCompositionRoot root, string name)
        {
            root.Context.RecipesInApi.Add(new Recipe { Name = name });
        }
    }

    
}