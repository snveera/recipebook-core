using recipebook.blazor.Models;
using recipebook.blazor.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recipebook.blazor.test.TestUtility.Fakes
{
    public class FakeRecipeService : IRecipeService
    {
        private readonly TestContext _context;

        public FakeRecipeService(TestContext context)
        {
            _context = context;
        }

        public Task Create(Recipe toSave)
        {
            _context.Recipes.Add(toSave);

            return Task.FromResult(true);
        }

        public Task Delete(string id)
        {
            var recipe = _context.Recipes.First(r => r.Id == id);
            _context.Recipes.Remove(recipe);

            return Task.FromResult(true);
        }

        public Task<Recipe> Get(string id)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            return Task.FromResult(recipe);
        }

        public Task<List<Recipe>> Search(string searchText, string category)
        {
            return Task.FromResult(_context.Recipes);
        }

        public Task Update(Recipe toSave)
        {
            Delete(toSave.Id);
            Create(toSave);

            return Task.FromResult(true);
        }
    }
}
