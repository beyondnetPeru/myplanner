using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPlanner.Plannings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBPlanCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SizeModelTypeValueSelected",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "SizeModelTypeFactorId",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "SizeModelTypeItemId");

            migrationBuilder.RenameColumn(
                name: "SizeModelTypeFactorCode",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "BusinessFeature",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "PlanCategoryId");

            migrationBuilder.RenameColumn(
                name: "BallParkTotalCost",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "BallparkDependenciesCostAmount");

            migrationBuilder.RenameColumn(
                name: "BallParkDependenciesCost",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "BallParkTotalCostAmount");

            migrationBuilder.RenameColumn(
                name: "BallParkCost",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "BallParkCostAmount");

            migrationBuilder.AlterColumn<string>(
                name: "SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "plans",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "myplanner-plannings",
                table: "plans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BallParkCostSymbol",
                schema: "myplanner-plannings",
                table: "planitems",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<string>(
                name: "BusinessFeatureComplexityLevel",
                schema: "myplanner-plannings",
                table: "planitems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BusinessFeatureDefinition",
                schema: "myplanner-plannings",
                table: "planitems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BusinessFeatureMoScoW",
                schema: "myplanner-plannings",
                table: "planitems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BusinessFeatureName",
                schema: "myplanner-plannings",
                table: "planitems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BusinessFeaturePriority",
                schema: "myplanner-plannings",
                table: "planitems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "plancategory",
                schema: "myplanner-plannings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlanId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plancategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_plancategory_plans_PlanId",
                        column: x => x.PlanId,
                        principalSchema: "myplanner-plannings",
                        principalTable: "plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_plans_SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "plans",
                column: "SizeModelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_plancategory_PlanId",
                schema: "myplanner-plannings",
                table: "plancategory",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_plans_sizemodeltypes_SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "plans",
                column: "SizeModelTypeId",
                principalSchema: "myplanner-plannings",
                principalTable: "sizemodeltypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_plans_sizemodeltypes_SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "plans");

            migrationBuilder.DropTable(
                name: "plancategory",
                schema: "myplanner-plannings");

            migrationBuilder.DropIndex(
                name: "IX_plans_SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "plans");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "myplanner-plannings",
                table: "plans");

            migrationBuilder.DropColumn(
                name: "BallParkCostSymbol",
                schema: "myplanner-plannings",
                table: "planitems");

            migrationBuilder.DropColumn(
                name: "BallParkTotalCostSymbol",
                schema: "myplanner-plannings",
                table: "planitems");

            migrationBuilder.DropColumn(
                name: "BallparkDependenciesCostSymbol",
                schema: "myplanner-plannings",
                table: "planitems");

            migrationBuilder.DropColumn(
                name: "BusinessFeatureComplexityLevel",
                schema: "myplanner-plannings",
                table: "planitems");

            migrationBuilder.DropColumn(
                name: "BusinessFeatureDefinition",
                schema: "myplanner-plannings",
                table: "planitems");

            migrationBuilder.DropColumn(
                name: "BusinessFeatureMoScoW",
                schema: "myplanner-plannings",
                table: "planitems");

            migrationBuilder.DropColumn(
                name: "BusinessFeatureName",
                schema: "myplanner-plannings",
                table: "planitems");

            migrationBuilder.DropColumn(
                name: "BusinessFeaturePriority",
                schema: "myplanner-plannings",
                table: "planitems");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "SizeModelTypeValueSelected");

            migrationBuilder.RenameColumn(
                name: "SizeModelTypeItemId",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "SizeModelTypeFactorId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "SizeModelTypeFactorCode");

            migrationBuilder.RenameColumn(
                name: "PlanCategoryId",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "BusinessFeature");

            migrationBuilder.RenameColumn(
                name: "BallparkDependenciesCostAmount",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "BallParkTotalCost");

            migrationBuilder.RenameColumn(
                name: "BallParkTotalCostAmount",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "BallParkDependenciesCost");

            migrationBuilder.RenameColumn(
                name: "BallParkCostAmount",
                schema: "myplanner-plannings",
                table: "planitems",
                newName: "BallParkCost");

            migrationBuilder.AlterColumn<string>(
                name: "SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "plans",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
