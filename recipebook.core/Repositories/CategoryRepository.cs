using System;
using System.Collections.Generic;
using System.Linq;
using recipebook.core.Models;
using recipebook.entityframework;

namespace recipebook.core.Repositories
{
    public class CategoryRepository
    {
        private readonly RecipeBookDbContext _dbContext;

        public CategoryRepository(RecipeBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyCollection<Category> Get()
        {
            var data = _dbContext.Categories;
            var result = data.Select(Map).ToList();

            return result;
        }

        public Category Map(entityframework.Models.Category toMap)
        {
            return new Category
            {
                Name = toMap.Name
            };
        }

        public Category Create(Category toCreate)
        {
            var dataModel = new entityframework.Models.Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = toCreate.Name
            };
            _dbContext.Categories.Add(dataModel);
            _dbContext.SaveChanges();

            return toCreate;
        }
    }
}