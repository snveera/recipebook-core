using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace recipebookcorewebapi.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Directions = table.Column<string>(nullable: true),
                    Ingredients = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: true),
                    Servings = table.Column<int>(nullable: true),
                    Source = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
