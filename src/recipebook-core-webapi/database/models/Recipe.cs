using System.ComponentModel.DataAnnotations;

namespace recipebook_core_webapi.database.models
{
    public class Recipe
    {
        [Key]
        public int RecipeId {get;set;}
        
        public string Name {get;set;}
        
        public int? Servings {get;set;}
        
        public int? Rating {get;set;}
        
        public string Ingredients {get;set;}
        
        public string Directions {get;set;}
        
        public string Source {get;set;}
    }
}