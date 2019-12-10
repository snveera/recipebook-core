using System;
using System.Collections.Generic;
using System.Linq;
using recipebook.core.Models;
using recipebook.entityframework;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace recipebook.core.Repositories
{
    public class RecipeRepository
    {
        private readonly RecipeBookDbContext _dbContext;

        public RecipeRepository(RecipeBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IReadOnlyCollection<Recipe> Get(string criteria, string category)
        {
            var query = _dbContext.Recipes
                .AsNoTracking();

            if(category != null)
            {
                query = query.Where(r => r.Category == category);
            }
            if (criteria != null)
            {
                query = query
                    .ToList() // Force this to run client side since the implementation (currently Cosmos) does not implement the contains
                    .AsQueryable()
                    .Where(r => r.Name.Contains(criteria) || r.Category.Contains(criteria));
            }

            var data = query
                .Select(Map)
                .ToImmutableList();

            return data;
        }

        public async Task<Recipe> Get(string id)
        {
            var item = await _dbContext.Recipes
                .FindAsync(id);
            var mappedItem = Map(item);

            return mappedItem;
        }

        public async Task<Recipe> Create(Recipe toCreate)
        {
            AssignIdentifier(toCreate);
            var dbModel = Map(toCreate);
            
            _dbContext.Recipes.Add(dbModel);
            await _dbContext.SaveChangesAsync();

            return await Get(toCreate.Id);
        }

        private static void AssignIdentifier(Recipe toCreate)
        {
            toCreate.Id = Guid.NewGuid().ToString();
        }

        public async Task<Recipe> Update(Recipe toUpdate)
        {
            var item = _dbContext.Recipes.Find(toUpdate.Id);
            if(item == null)
            {
                var response = Create(toUpdate);
                return await response;
            }
            else
            {
                UpdateData(toUpdate, item);
                await _dbContext.SaveChangesAsync();

                var response = Map(item);
                return response;
            }

        }

        private static Recipe Map(entityframework.Models.Recipe toMap)
        {
            return new Recipe
            {
                Id = toMap.Id,
                Name = toMap.Name,
                Servings = toMap.Servings,
                Rating = toMap.Rating,
                Ingredients = toMap.Ingredients,
                Directions = toMap.Directions,
                Source = toMap.Source,
                Category = toMap.Category
            };
        }

        private static entityframework.Models.Recipe Map(Recipe toMap)
        {
            return new entityframework.Models.Recipe
            {
                Id = toMap.Id,
                Name = toMap.Name,
                Servings = toMap.Servings,
                Rating = toMap.Rating,
                Ingredients = toMap.Ingredients,
                Directions = toMap.Directions,
                Source = toMap.Source,
                Category = toMap.Category
            };
        }

        private static void UpdateData(Recipe from, entityframework.Models.Recipe to)
        {

            to.Name = from.Name;
            to.Servings = from.Servings;
            to.Rating = from.Rating;
            to.Ingredients = from.Ingredients;
            to.Directions = from.Directions;
            to.Source = from.Source;
            to.Category = from.Category;

        }
    }
}
