using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderInColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Columns",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Columns");
        }
    }
}
