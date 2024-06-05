using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlanner.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInstructionsDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Recipes\" ALTER COLUMN \"Instructions\" TYPE text[] USING string_to_array(\"Instructions\", ',')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Recipes\" ALTER COLUMN \"Instructions\" TYPE text USING array_to_string(\"Instructions\", ',')");
        }
    }
}
