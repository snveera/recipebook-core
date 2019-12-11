using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Directus.SimpleDb.Attributes;
using Directus.SimpleDb.Providers;
using Newtonsoft.Json;
using Xunit;

namespace recipebook.datatransfer
{
    public class DomusToRecipeBook
    {
        [Fact]
        public void LoadData()
        {
            var existingRecipes = GetDomusRecipes();
            var dataAsJson = JsonConvert.SerializeObject(existingRecipes);

            File.WriteAllText(@"c:\temp\recipe-data.json",dataAsJson);

        }

        private static ICollection<DomusRecipe> GetDomusRecipes()
        {
            var key = "";
            var secret = "";
            var simpleDbProvider = new SimpleDBProvider<DomusRecipe, string>(key, secret);
            var existingRecipes = simpleDbProvider.Get();
            return existingRecipes;
        }

        //private  Map(DomusRecipe toMap)
        //{
            
        //}
    }

    /// <summary>
    /// Domain model for a Recipe
    /// </summary>
    [Serializable]
    [DomainName("Domus_Recipe")]
    public class DomusRecipe
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key]
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
