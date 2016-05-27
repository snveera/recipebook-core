using Microsoft.EntityFrameworkCore;
using recipebook_core_webapi.database.models;

namespace recipebook_core_webapi.database
{
    public class RecipebookDbContext:DbContext
    {
        public DbSet<Recipe> Recipes {get;set;}
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./recipebook.db");
        }
    }
}