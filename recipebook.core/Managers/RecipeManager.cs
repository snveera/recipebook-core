using recipebook.core.Models;
using recipebook.core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace recipebook.core.Managers
{
    public class RecipeManager
    {
        private RecipeRepository _repository;

        public RecipeManager(RecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<Recipe>> Search(string criteria, string category)
        {
            var resolvedCriteria = string.IsNullOrWhiteSpace(criteria) ? null : criteria;
            var resolvedCategory = string.IsNullOrWhiteSpace(category) ? null : category;

            var data = _repository.Get(criteria, category);

            return data;
        }

        public async Task<Recipe> GetById(string id)
        {
            var item = await _repository.Get(id);

            return item;
        }

        public async Task<Recipe> Create(Recipe toSave)
        {
            var response = await _repository.Create(toSave);

            return response;
        }

        public async Task<Recipe> Update(Recipe toUpdate)
        {
            var response = await _repository.Update(toUpdate);

            return response;
        }
    }
}
