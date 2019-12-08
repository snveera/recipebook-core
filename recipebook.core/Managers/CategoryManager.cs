using System.Collections.Generic;
using recipebook.core.Models;
using recipebook.core.Repositories;

namespace recipebook.core.Managers
{
    public class CategoryManager
    {
        private readonly CategoryRepository _repository;

        public CategoryManager(CategoryRepository repository)
        {
            _repository = repository;
        }

        public IReadOnlyCollection<Category> Get()
        {
            return _repository.Get();
        }

        public Category Create(string name)
        {
            var category = new Category
            {
                Name = name
            };
            _repository.Create(category);

            return category;
        }
    }
}