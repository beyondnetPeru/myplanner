using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPlanner.Plannings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PlanItemAndPlanRemoveUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "myplanner-plannings",
                table: "plans");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "myplanner-plannings",
                table: "planitems");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessFeatureMoScoW",
                schema: "myplanner-plannings",
                table: "planitems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessFeatureComplexityLevel",
                schema: "myplanner-plannings",
                table: "planitems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "myplanner-plannings",
                table: "plans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessFeatureMoScoW",
                schema: "myplanner-plannings",
                table: "planitems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessFeatureComplexityLevel",
                schema: "myplanner-plannings",
                table: "planitems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "myplanner-plannings",
                table: "planitems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
