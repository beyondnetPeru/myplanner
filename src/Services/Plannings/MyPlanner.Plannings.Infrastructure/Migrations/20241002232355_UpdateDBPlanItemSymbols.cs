using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPlanner.Plannings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBPlanItemSymbols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BallParkTotalCostSymbol",
                schema: "myplanner-plannings",
                table: "planitems");

            migrationBuilder.DropColumn(
                name: "BallparkDependenciesCostSymbol",
                schema: "myplanner-plannings",
                table: "planitems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BallParkTotalCostSymbol",
                schema: "myplanner-plannings",
                table: "planitems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BallparkDependenciesCostSymbol",
                schema: "myplanner-plannings",
                table: "planitems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
