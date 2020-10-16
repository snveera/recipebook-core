using recipebook.blazor.Models;
using recipebook.blazor.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace recipebook.blazor.Repositories
{
    public class CategoryRepository
    {
        private readonly ICategoryService _categoryService;

        public CategoryRepository(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        private ICollection<CategoryViewModel> _cachedData = null;
        public async Task<ICollection<CategoryViewModel>> GetAsync()
        {
            if(_cachedData == null)
            {
                var data = await _categoryService.Get();
                _cachedData = Map(data).ToList();
            }
            return _cachedData;
        }

        private IEnumerable<CategoryViewModel> Map(IEnumerable<Category> toMap)
        {
            foreach(var item in toMap)
            {
                yield return new CategoryViewModel { Name = item.Name };
            }
        }
    }
}
