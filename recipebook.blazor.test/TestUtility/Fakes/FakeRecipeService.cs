using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using recipebook.blazorclient.Application.Models;
using recipebook.blazorclient.Application.Services;

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
            return _context.RecipesInApi;
        }

        public async Task<Recipe> GetById(string id)
        {
            return _context.RecipesInApi.FirstOrDefault(r => r.Id == id);
        }
    }
}