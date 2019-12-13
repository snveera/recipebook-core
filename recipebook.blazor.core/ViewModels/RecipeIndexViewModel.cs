﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using recipebook.blazor.core.Models;
using recipebook.blazor.core.Services;

namespace recipebook.blazor.core.ViewModels
{
    public class RecipeIndexViewModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IRecipeService _recipeService;

        private bool _isLoadingRecipes = false;

        public RecipeIndexViewModel(ICategoryService categoryService, IRecipeService recipeService)
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

        private static CategoryViewModel Map(Category toMap)
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

        private static RecipeViewModel Map(Recipe toMap)
        {
            return new RecipeViewModel
            {
                Id = toMap.Id,
                Name = toMap.Name?.Trim(),
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

        public string Url { get; set; }
    }
}