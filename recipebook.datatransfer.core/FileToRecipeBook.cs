using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using recipebook.entityframework;
using recipebook.entityframework.Models;
using Xunit;

namespace recipebook.datatransfer.core
{
    public class FileToRecipeBook
    {
        [Fact]
        public async Task LoadDataToRecipeBook()
        {
            var fileData = File.ReadAllText(@"c:\temp\recipe-data.json");
            var recipes = JsonConvert.DeserializeObject<ICollection<DomusRecipe>>(fileData);

            var mappedRecipes = recipes
                .Where(r=>!r.IsDeleted)
                .Select(Map)
                .ToList();

            var context = GetContext();

            await context.Recipes.AddRangeAsync(mappedRecipes);
            await context.SaveChangesAsync();

        }

        private RecipeBookDbContext GetContext()
        {
            var builder = new DbContextOptionsBuilder<RecipeBookDbContext>();
            var accountEndpoint = "";
            var accountKey = "";
            var databaseName = "";

            builder.UseCosmos(accountEndpoint, accountKey, databaseName);

            return new RecipeBookDbContext(builder.Options);
        }

        private recipebook.entityframework.Models.Recipe Map(DomusRecipe toMap)
        {
            return new Recipe
            {
                Id = toMap.RecipeId,
                Category = toMap.Category,
                Directions = toMap.Directions,
                Ingredients = toMap.Ingredients,
                Servings = ToInt(toMap.Servings),
                Source = toMap.Source,
                Rating = toMap.Rating,
                Name = toMap.Name,
                CreatedAt = DateTime.Now,
                CreatedBy = "import",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "import"
            };
        }
        private int? ToInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            var parsed = Int32.TryParse(value, out int result);
            if(parsed)
                return result;
            return null;
        }

    }

    /// <summary>
    /// Domain model for a Recipe
    /// </summary>
    public class DomusRecipe
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public virtual string RecipeId { get; set; }

        /// <summary>
        /// Name the recipe is commonly referred to as
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Number of servings this recipe produces
        /// </summary>
        public virtual string Servings { get; set; }

        /// <summary>
        /// Current rating for the recipe
        /// </summary>
        public virtual int? Rating { get; set; }

        /// <summary>
        /// Category this recipe falls into
        /// </summary>
        public virtual string Category { get; set; }

        /// <summary>
        /// Category this recipe used to be in
        /// </summary>
        public virtual string PreviousCategory { get; set; }

        /// <summary>
        /// Ingredients used to make the food item this recipe is for
        /// </summary>
        public virtual string Ingredients { get; set; }

        /// <summary>
        /// How to make the food item
        /// </summary>
        public virtual string Directions { get; set; }

        /// <summary>
        /// Where this recipe came from
        /// </summary>
        public virtual string Source { get; set; }

        /// <summary>
        /// If this recipe is deleted or not
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// Url where the recipe's image is at
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
