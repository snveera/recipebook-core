using System.Threading.Tasks;
using recipebook.blazorclient.Application.Services;

namespace recipebook.blazorclient.Application.ViewModels
{
    public class RecipeDetailViewModel
    {
        private readonly RecipeService _recipeService;

        public RecipeDetailViewModel(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        public RecipeViewModel Recipe { get; set; }

        public string Title => Recipe?.Name ?? "...";

        public async Task Initialize(string recipeId)
        {
            await LoadRecipe(recipeId);
        }

        private async Task LoadRecipe(string recipeId)
        {
        }
    }
}