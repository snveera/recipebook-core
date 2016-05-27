using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using recipebook_core_webapi.Controllers;
using recipebook_core_webapi.models;
using recipebook_core_webapi.database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using recipebook_core_webapi.database.models;

namespace recipebook_core_test
{
    public class RecipeControllerTests
    {
        [Fact]
        public void Get_NoArguments_ReturnsAllRecipes()
        {
            // Arrange
            var dbcontext = GetDbContext();
            SeedTestData(dbcontext);
            var controller = new RecipeController(dbcontext);
            
            // Act
            var result = controller.Get();
            
            // Assert
            Assert.NotEmpty(result);
        }
        
        [Fact]
        public void Get_WithIdentifier_ReturnsMatchingRecipes()
        {
            // Arrange
            var dbcontext = GetDbContext();
            SeedTestData(dbcontext);
            var controller = new RecipeController(dbcontext);
            
            // Act
            var result = controller.Get(1);
            
            // Assert
            Assert.Equal(result.RecipeId,1);
        }
        
        [Fact]
        public void Get_WithInvalidIdentifier_ReturnsNothing()
        {
            // Arrange
            var dbcontext = GetDbContext();
            SeedTestData(dbcontext);
            var controller = new RecipeController(dbcontext);
            
            // Act
            var result = controller.Get(100);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void Put_WithValidId_UpdatesRecipe()
        {
            // Arrange
            var dbcontext = GetDbContext();
            SeedTestData(dbcontext);
            var controller = new RecipeController(dbcontext);
            
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
            var dbcontext = GetDbContext();
            SeedTestData(dbcontext);
            var controller = new RecipeController(dbcontext);
            
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
           var dbcontext = GetDbContext();
            SeedTestData(dbcontext);
            var controller = new RecipeController(dbcontext);
          
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
            var dbcontext = GetDbContext();
            SeedTestData(dbcontext);
            var controller = new RecipeController(dbcontext);
            
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
            var dbcontext = GetDbContext();
            SeedTestData(dbcontext);
            var controller = new RecipeController(dbcontext);
            
            var recipesBefore = controller.Get().ToList();
            
            // Act
            controller.Delete(100);
            
            // Assert
            var recipesAfter = controller.Get().ToList();
            Assert.Equal(recipesBefore.Count(),recipesAfter.Count());
        }
        
        private RecipebookDbContext GetDbContext()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<RecipebookDbContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);
                   
            return new RecipebookDbContext(builder.Options);
        }
        
        private void SeedTestData(RecipebookDbContext dbcontext)
        {
            dbcontext.Recipes.Add(new Recipe{Name="one"});
            dbcontext.Recipes.Add(new Recipe{Name="two"});
            
            dbcontext.SaveChanges();
        }
    }
}
