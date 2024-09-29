using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameShowAddCardByDefaultToIsAlwaysVisibleAddCardButton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShowAddCardByDefault",
                table: "Columns",
                newName: "IsAlwaysVisibleAddCardButton");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAlwaysVisibleAddCardButton",
                table: "Columns",
                newName: "ShowAddCardByDefault");
        }
    }
}
