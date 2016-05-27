using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using recipebook_core_webapi.Controllers;
using recipebook_core_webapi.models;

namespace recipebook_core_test
{
    public class RecipeControllerTests
    {
        [Fact]
        public void Get_NoArguments_ReturnsAllRecipes()
        {
            // Arrange
            var controller = new RecipeController();
            
            // Act
            var result = controller.Get();
            
            // Assert
            Assert.NotEmpty(result);
        }
        
        [Fact]
        public void Get_WithIdentifier_ReturnsMatchingRecipes()
        {
            // Arrange
            var controller = new RecipeController();
            
            // Act
            var result = controller.Get(1);
            
            // Assert
            Assert.Equal(result.RecipeId,1);
        }
        
        [Fact]
        public void Get_WithInvalidIdentifier_ReturnsNothing()
        {
            // Arrange
            var controller = new RecipeController();
            
            // Act
            var result = controller.Get(100);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void Put_WithValidId_UpdatesRecipe()
        {
            // Arrange
            var controller = new RecipeController();
            
            var recipe = new RecipeApiModel{Name = "something else"};
            var recipeId = 1;
            
            // Act
            controller.Put(recipeId, recipe);
            
            // Assert
            var recipeAfterUpdate = controller.Get(recipeId);
            Assert.Equal("something else",recipeAfterUpdate.Name);
        }
        
        [Fact]
        public void Put_WithInValidId_DoesNothing()
        {
            // Arrange
            var controller = new RecipeController();
            
            var recipe = new RecipeApiModel{Name = "something else"};
            var recipeId = 100;
            
            // Act
            controller.Put(recipeId, recipe);
            
            // Assert
            var recipesAfterPut = controller.Get().ToList();
            Assert.False(recipesAfterPut.Any(r=>r.Name == "something else"));
        }
        
        [Fact]
        public void Post_WithBody_AddsRecipe()
        {
            // Arrange
            var controller = new RecipeController();
          
            var recipe = new RecipeApiModel{
                Name = "my random name",
            };
            
          
            // Act
            controller.Post(recipe);
            
            // Assert
            var recipesAfter = controller.Get().ToList();
            
            var postedRecipeExists = recipesAfter.Any(r=>r.Name == "my random name");
            Assert.True(postedRecipeExists);
            
        }
        
        [Fact]
        public void Delete_WithValidIdentifier_RemovesRecipe()
        {
            // Arrange
            var controller = new RecipeController();
            
            var recipesBefore = controller.Get().ToList();
            
            // Act
            controller.Delete(1);
            
            // Assert
            var recipesAfter = controller.Get().ToList();
             Assert.Equal(recipesBefore.Count()-1,recipesAfter.Count());
        }
        
        [Fact]
        public void Delete_WithInvalidIdentifier_DoesNothing()
        {
            // Arrange
            var controller = new RecipeController();
            
            var recipesBefore = controller.Get().ToList();
            
            // Act
            controller.Delete(100);
            
            // Assert
            var recipesAfter = controller.Get().ToList();
            Assert.Equal(recipesBefore.Count(),recipesAfter.Count());
        }
    }
}
