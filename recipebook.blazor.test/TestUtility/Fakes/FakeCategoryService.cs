using recipebook.blazor.Models;
using recipebook.blazor.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace recipebook.blazor.test.TestUtility.Fakes
{
    public class FakeCategoryService : ICategoryService
    {
        private readonly TestContext _context;

        public FakeCategoryService(TestContext context)
        {
            _context = context;
        }
        public Task<List<Category>> Get()
        {
            return Task.FromResult(_context.Categories);
        }
    }
}
