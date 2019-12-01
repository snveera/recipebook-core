﻿using System;
using recipebook.entityframework;

namespace recipebook.functions.test.TestUtility.Extensions
{
    public static class DataExtensions
    {
        public static entityframework.Models.Recipe WithRecipe(this TestCompositionRoot root,
            string name,
            string ingredients = null,
            string directions = null,
            int? servings = null,
            string source = null,
            int? rating = null,
            string id = null)
        {
            var context = root.Get<RecipeBookDbContext>();

            var entity = new entityframework.Models.Recipe
            {
                Id = id ?? Guid.NewGuid().ToString(),
                Name = name,
                Ingredients = ingredients,
                Directions = directions,
                Servings = servings,
                Source = source,
                Rating = rating
            };
            context.Recipes.Add(entity);

            context.SaveChanges();

            return entity;
        }
    }
}