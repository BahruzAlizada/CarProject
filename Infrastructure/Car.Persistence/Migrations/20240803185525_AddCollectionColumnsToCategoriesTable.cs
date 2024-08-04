using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCollectionColumnsToCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Collection",
                table: "Categories",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Collection",
                table: "Categories");
        }
    }
}
