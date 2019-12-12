using System.Collections.Generic;
using System.Threading.Tasks;
using recipebook.blazorclient.Application.Extensions;
using recipebook.blazorclient.Application.Models;
using recipebook.blazorclient.Application.Services;

namespace recipebook.blazorclient.Application.ViewModels
{
    public class RecipeDetailViewModel
    {
        private readonly IRecipeService _recipeService;
        private Recipe _recipe;

        public RecipeDetailViewModel(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public string Id => _recipe?.Id;
        public string Title => _recipe?.Name?.Trim() ?? "Loading...";
        public string Servings => _recipe?.Servings?.ToString() ?? "";
        public List<string> Ingredients => _recipe?.Ingredients?.ToLineList() ?? new List<string>();
        public List<string> Directions => _recipe?.Directions?.ToLineList() ?? new List<string>();
        public string Source => _recipe?.Source?.Trim() ?? "";
        public int? Rating => _recipe?.Rating;

        public async Task Initialize(string recipeId)
        {
            await LoadRecipe(recipeId);
        }

        private async Task LoadRecipe(string recipeId)
        {
            this._recipe = await _recipeService.GetById(recipeId);
        }
    }
}