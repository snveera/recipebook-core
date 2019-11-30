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

        public IReadOnlyCollection<Recipe> Get()
        {
            var data = _repository.Get();

            return data;
        }

        public Recipe Get(string id)
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
