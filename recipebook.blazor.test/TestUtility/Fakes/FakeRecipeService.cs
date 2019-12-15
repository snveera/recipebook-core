using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using recipebook.blazor.core.Models;
using recipebook.blazor.core.Services;

namespace recipebook.blazor.test.TestUtility.Fakes
{
    public class FakeRecipeService:IRecipeService
    {
        private readonly TestContext _context;

        public FakeRecipeService(TestContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Recipe>> Get(string criteria, string category)
        {
            _context.RecipeCriteriaSentToApi = criteria;
            _context.RecipeCategorySentToApi = category;
            return _context.RecipesInApi;
        }

        public async Task<Recipe> GetById(string id)
        {
            _context.RecipeId = id;
            return _context.RecipesInApi.FirstOrDefault(r => r.Id == id);
        }

        public async Task<Recipe> Create(Recipe recipeData)
        {
            recipeData.Id = Guid.NewGuid().ToString();
            _context.RecipesInApi.Add(recipeData);
            return recipeData;
        }

        public async Task<Recipe> Update(Recipe recipeData)
        {
            var existing = await GetById(recipeData.Id);
            if(existing!=null)
                _context.RecipesInApi.Remove(existing);

            _context.RecipesInApi.Add(recipeData);
            return recipeData;
        }
    }
}