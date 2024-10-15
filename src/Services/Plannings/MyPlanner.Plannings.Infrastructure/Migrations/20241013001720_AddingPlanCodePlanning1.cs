using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPlanner.Plannings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingPlanCodePlanning1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_plancategory_plans_PlanId",
                schema: "myplanner-plannings",
                table: "plancategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_plancategory",
                schema: "myplanner-plannings",
                table: "plancategory");

            migrationBuilder.RenameTable(
                name: "plancategory",
                schema: "myplanner-plannings",
                newName: "plancategories",
                newSchema: "myplanner-plannings");

            migrationBuilder.RenameIndex(
                name: "IX_plancategory_PlanId",
                schema: "myplanner-plannings",
                table: "plancategories",
                newName: "IX_plancategories_PlanId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "myplanner-plannings",
                table: "plans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_plancategories",
                schema: "myplanner-plannings",
                table: "plancategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_plancategories_plans_PlanId",
                schema: "myplanner-plannings",
                table: "plancategories",
                column: "PlanId",
                principalSchema: "myplanner-plannings",
                principalTable: "plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_plancategories_plans_PlanId",
                schema: "myplanner-plannings",
                table: "plancategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_plancategories",
                schema: "myplanner-plannings",
                table: "plancategories");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "myplanner-plannings",
                table: "plans");

            migrationBuilder.RenameTable(
                name: "plancategories",
                schema: "myplanner-plannings",
                newName: "plancategory",
                newSchema: "myplanner-plannings");

            migrationBuilder.RenameIndex(
                name: "IX_plancategories_PlanId",
                schema: "myplanner-plannings",
                table: "plancategory",
                newName: "IX_plancategory_PlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_plancategory",
                schema: "myplanner-plannings",
                table: "plancategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_plancategory_plans_PlanId",
                schema: "myplanner-plannings",
                table: "plancategory",
                column: "PlanId",
                principalSchema: "myplanner-plannings",
                principalTable: "plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
