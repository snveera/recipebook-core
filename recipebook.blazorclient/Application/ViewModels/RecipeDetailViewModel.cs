using System.Threading.Tasks;
using recipebook.blazorclient.Application.Models;
using recipebook.blazorclient.Application.Services;

namespace recipebook.blazorclient.Application.ViewModels
{
    public class RecipeDetailViewModel
    {
        private readonly RecipeService _recipeService;
        private Recipe _recipe;

        public RecipeDetailViewModel(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public string Title => _recipe?.Name ?? "Loading...";
        public string Servings => _recipe?.Servings?.ToString() ?? "";
        public string Ingredients => _recipe?.Ingredients ?? "";
        public string Directions => _recipe?.Directions ?? "";
        public string Source => _recipe?.Source ?? "";

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