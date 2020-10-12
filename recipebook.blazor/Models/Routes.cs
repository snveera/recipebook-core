using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recipebook.blazor.Models
{
    public static class Routes
    {
        public static string SearchResults = "";
        public static string RecipeEdit = "recipes/{0}/edit";
        public static string RecipeCreate = "recipes/create";
        public static string RecipeDetails = "recipes/{0}";
    }
}
