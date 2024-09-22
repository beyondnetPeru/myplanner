using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPlanner.Plannings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringSizeModelsIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SizeModelFactorSelected",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                newName: "SizeModelTypeSelected");

            migrationBuilder.RenameColumn(
                name: "ProfileValueSelected",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                newName: "SizeModelTypeItemId");

            migrationBuilder.RenameColumn(
                name: "ProfileCountValue",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "ProfileAvgRateAmount",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                newName: "ProfileAvgRateValue");

            migrationBuilder.AddColumn<int>(
                name: "FactorSelected",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfileAvgRateSymbol",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FactorSelected",
                schema: "myplanner-plannings",
                table: "sizemodelitems");

            migrationBuilder.DropColumn(
                name: "ProfileAvgRateSymbol",
                schema: "myplanner-plannings",
                table: "sizemodelitems");

            migrationBuilder.RenameColumn(
                name: "SizeModelTypeSelected",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                newName: "SizeModelFactorSelected");

            migrationBuilder.RenameColumn(
                name: "SizeModelTypeItemId",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                newName: "ProfileValueSelected");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                newName: "ProfileCountValue");

            migrationBuilder.RenameColumn(
                name: "ProfileAvgRateValue",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                newName: "ProfileAvgRateAmount");
        }
    }
}
