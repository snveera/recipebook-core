using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using recipebook.blazor.core.Extensions;
using recipebook.blazor.core.Models;
using recipebook.blazor.core.Services;

namespace recipebook.blazor.core.ViewModels
{
    public class RecipeEditViewModel
    {
        private readonly IRecipeService _recipeService;
        private readonly ICategoryService _categoryService;
        private readonly NavigationManager _navigationManager;

        public RecipeEditViewModel(IRecipeService recipeService, 
            ICategoryService categoryService,
            NavigationManager navigationManager)
        {
            _recipeService = recipeService;
            _categoryService = categoryService;
            _navigationManager = navigationManager;
        }

        public string Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public int? Servings { get; set; }
        public string Ingredients { get; set; }
        public string Directions { get; set; }
        public string Source { get; set; }
        public int? Rating { get; set; }

        public List<string> Categories { get; private set; } = new List<string>();

        public async Task Initialize(string recipeId)
        {
            await LoadCategories();
            await LoadRecipe(recipeId);
        }

        public async Task Save()
        {
            var recipeData = MapFromViewModel();

            if(string.IsNullOrWhiteSpace(Id))
            {
                await _recipeService.Create(recipeData);
            }
            else
            {
                await _recipeService.Update(recipeData);
            }

            _navigationManager.NavigateTo("/recipe");
        }

        private async Task LoadCategories()
        {
            var categories = await _categoryService.Get();
            var categoryNames = categories
                .Select(c => c.Name)
                .OrderBy(c => c)
                .ToList();
            this.Categories = categoryNames;
        }

        private async Task LoadRecipe(string recipeId)
        {
            Id = recipeId;
            if (!string.IsNullOrWhiteSpace(recipeId))
            {
                var recipe = await _recipeService.GetById(recipeId) ?? new Recipe {Id = recipeId, Name = "Not Found"};

                MapToViewModel(recipe);
            }
        }

        private void MapToViewModel(Recipe recipe)
        {
            Category = recipe.Category;
            Name = recipe.Name;
            Servings = recipe.Servings;
            Ingredients = recipe.Ingredients;
            Directions = recipe.Directions;
            Source = recipe.Source;
            Rating = recipe.Rating;
        }
        private Recipe MapFromViewModel()
        {
            var recipeData = new Recipe
            {
                Id = Id,
                Category = Category,
                Name = Name,
                Servings = Servings,
                Ingredients = Ingredients,
                Directions = Directions,
                Source = Source,
                Rating = Rating
            };
            return recipeData;
        }
    }
}