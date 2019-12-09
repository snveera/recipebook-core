using recipebook.core.Models;
using recipebook.core.Repositories;
using System.Collections.Generic;

namespace recipebook.core.Managers
{
    public class RecipeManager
    {
        private RecipeRepository _repository;

        public RecipeManager(RecipeRepository repository)
        {
            _repository = repository;
        }

        public IReadOnlyCollection<Recipe> Search(string criteria, string category)
        {
            var resolvedCriteria = string.IsNullOrWhiteSpace(criteria) ? null : criteria;
            var resolvedCategory = string.IsNullOrWhiteSpace(category) ? null : category;

            var data = _repository.Get(criteria, category);

            return data;
        }

        public Recipe GetById(string id)
        {
            var item = _repository.Get(id);

            return item;
        }

        public Recipe Create(Recipe toSave)
        {
            var response = _repository.Create(toSave);

            return response;
        }

        public Recipe Update(Recipe toUpdate)
        {
            var response = _repository.Update(toUpdate);

            return response;
        }
    }
}
