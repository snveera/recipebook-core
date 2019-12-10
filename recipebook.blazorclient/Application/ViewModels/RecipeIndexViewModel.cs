using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using recipebook.blazorclient.Application.Services;

namespace recipebook.blazorclient.Application.ViewModels
{
    public class RecipeIndexViewModel
    {
        private readonly CategoryService _categoryService;
        private readonly RecipeService _recipeService;

        private bool _isLoadingRecipes = true;

        public RecipeIndexViewModel(CategoryService categoryService, RecipeService recipeService)
        {
            _categoryService = categoryService;
            _recipeService = recipeService;
        }
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public List<RecipeViewModel> Recipes { get; set; } = new List<RecipeViewModel>();

        public string CategoryStatus { 
            get
            {
                return Categories.Any() ? "" : "disabled";
            } 
        }

        public string RecipeSearchStatus
        {
            get
            {
                return _isLoadingRecipes ? "disabled" : "";
            }
        }

        public string SearchTerms { get; set; } = "";
        public string Category { get; set; } = "";

        public async Task Search()
        {
            await LoadRecipes();
        }
        public async Task InitializeParameters(string category)
        {
            this.Category = category;
            if (!string.IsNullOrWhiteSpace(category))
            {
                await LoadRecipes();
            }
        }

        public async Task Initialize()
        {
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            var categories = await _categoryService.Get();
            this.Categories = categories
                .Select(Map)
                .OrderBy(c=>c.Name)
                .ToList();
        }

        private static CategoryViewModel Map(Models.Category toMap)
        {
            return new CategoryViewModel
            {
                Name = toMap.Name,
                Url = $"/category/{toMap.Name}"
            };
        }

        private async Task LoadRecipes()
        {
            _isLoadingRecipes = true;
            try
            {
                var data = await _recipeService.Get(this.SearchTerms,this.Category);
                var viewModels = data
                    .Select(Map)
                    .OrderBy(r => r.Name)
                    .ToList();
                this.Recipes = viewModels;
            }
            finally
            {
                _isLoadingRecipes = false;
            }

        }

        private static RecipeViewModel Map(Models.Recipe toMap)
        {
            return new RecipeViewModel
            {
                Id = toMap.Id,
                Name = toMap.Name,
                Category = toMap.Category,
                Servings = toMap.Servings,
                Ingredients = toMap.Ingredients,
                Directions = toMap.Directions,
                Rating = toMap.Rating,
                Source = toMap.Source,
                Url = $"/recipe/{toMap.Id}"
            };
        }
    }

    public class CategoryViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class RecipeViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int? Servings { get; set; }

        public int? Rating { get; set; }

        public string Ingredients { get; set; }

        public string Directions { get; set; }

        public string Source { get; set; }

        public string Category { get; set; }

        public string Url { get; set; }
    }
}