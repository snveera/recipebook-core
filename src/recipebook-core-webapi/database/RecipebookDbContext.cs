using Microsoft.EntityFrameworkCore;
using recipebook_core_webapi.database.models;

namespace recipebook_core_webapi.database
{
    public class RecipebookDbContext:DbContext
    {
        public RecipebookDbContext()
        {
            
        }
       
        public RecipebookDbContext(DbContextOptions<RecipebookDbContext> options)
            : base(options)
        { 
            
        }
        
        public DbSet<Recipe> Recipes {get;set;}

    }
}