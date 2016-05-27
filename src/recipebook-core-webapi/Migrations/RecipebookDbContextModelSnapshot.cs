using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using recipebook_core_webapi.database;

namespace recipebookcorewebapi.Migrations
{
    [DbContext(typeof(RecipebookDbContext))]
    partial class RecipebookDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20896");

            modelBuilder.Entity("recipebook_core_webapi.database.models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Directions");

                    b.Property<string>("Ingredients");

                    b.Property<string>("Name");

                    b.Property<int?>("Rating");

                    b.Property<int?>("Servings");

                    b.Property<string>("Source");

                    b.HasKey("RecipeId");

                    b.ToTable("Recipes");
                });
        }
    }
}
