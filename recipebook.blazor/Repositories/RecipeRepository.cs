﻿using recipebook.blazor.Extensions;
using recipebook.blazor.Models;
using recipebook.blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recipebook.blazor.Repositories
{
    public class RecipeRepository
    {
        private readonly RecipeService _recipeService;

        public RecipeRepository(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public async Task<List<RecipeViewModel>> SearchAsync(string searchText, string category)
        {
            var data = await _recipeService.Search(searchText, category);

            var result = data?.Select(Map)?.ToList();

            return result;
        }

        public async Task<RecipeViewModel> GetAsync(string id)
        {
            var data = await _recipeService.Get(id);

            var result = Map(data);

            return result;
        }

        public Task SaveAsync(RecipeViewModel toSave)
        {
            var dataToSave = Map(toSave);
            if (toSave.Id == null) return _recipeService.Create(dataToSave);

            return _recipeService.Update(dataToSave);
        }

        private RecipeViewModel Map(Recipe toMap)
        {
            return new RecipeViewModel 
            { 
                Id = toMap?.Id,
                Name = toMap?.Name ?? "Not Found",
                Category = toMap?.Category,
                Servings = toMap?.Servings,
                Source = toMap?.Source,
                IngredientsRaw = toMap?.Ingredients,
                DirectionsRaw = toMap?.Directions
            };
        }

        private Recipe Map(RecipeViewModel toMap)
        {
            return new Recipe
            {
                Id = toMap?.Id,
                Name = toMap?.Name ?? "Not Found",
                Category = toMap?.Category,
                Servings = toMap?.Servings,
                Source = toMap?.Source,
                Ingredients = toMap?.IngredientsRaw,
                Directions = toMap?.IngredientsRaw
            };
        }
    }
}
