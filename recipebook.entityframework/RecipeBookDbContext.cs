using recipebook.entityframework.Models;

using Microsoft.EntityFrameworkCore;

namespace recipebook.entityframework
{
    public class RecipeBookDbContext : DbContext
    {
        public RecipeBookDbContext(DbContextOptions<RecipeBookDbContext> options) : base(options)
        {
            //use steps defined at
            // https://docs.microsoft.com/en-us/ef/core/providers/cosmos/?tabs=dotnet-core-cli
        }

        public virtual DbSet<Recipe> Recipes { get; set; }
    }
}
