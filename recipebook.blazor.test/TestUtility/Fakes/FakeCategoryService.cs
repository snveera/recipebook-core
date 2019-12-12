using System.Collections.Generic;
using System.Threading.Tasks;
using recipebook.blazor.core.Models;
using recipebook.blazor.core.Services;

namespace recipebook.blazor.test.TestUtility.Fakes
{
    public class FakeCategoryService:ICategoryService
    {
        private readonly TestContext _context;

        public FakeCategoryService(TestContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Category>> Get()
        {
            return _context.CategoriesInApi;
        }
    }
}