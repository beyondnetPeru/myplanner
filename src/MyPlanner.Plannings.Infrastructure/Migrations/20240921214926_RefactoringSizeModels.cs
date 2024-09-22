using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPlanner.Plannings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringSizeModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sizemodels_sizemodeltypes_SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "sizemodels");

            migrationBuilder.DropIndex(
                name: "IX_sizemodels_SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "sizemodels");

            migrationBuilder.DropColumn(
                name: "SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "sizemodels");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "myplanner-plannings",
                table: "sizemodelitems");

            migrationBuilder.DropColumn(
                name: "SizeModelTypeFactorCode",
                schema: "myplanner-plannings",
                table: "sizemodelitems");

            migrationBuilder.RenameColumn(
                name: "SizeModelTypeFactorId",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                newName: "SizeModelFactorSelected");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                newName: "ProfileCountValue");

            migrationBuilder.AddColumn<bool>(
                name: "IsStandard",
                schema: "myplanner-plannings",
                table: "sizemodels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ProfileValueSelected",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<bool>(
                name: "IsStandard",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStandard",
                schema: "myplanner-plannings",
                table: "sizemodels");

            migrationBuilder.DropColumn(
                name: "IsStandard",
                schema: "myplanner-plannings",
                table: "sizemodelitems");

            migrationBuilder.RenameColumn(
                name: "SizeModelFactorSelected",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                newName: "SizeModelTypeFactorId");

            migrationBuilder.RenameColumn(
                name: "ProfileCountValue",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                newName: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "sizemodels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "ProfileValueSelected",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SizeModelTypeFactorCode",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_sizemodels_SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "sizemodels",
                column: "SizeModelTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_sizemodels_sizemodeltypes_SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "sizemodels",
                column: "SizeModelTypeId",
                principalSchema: "myplanner-plannings",
                principalTable: "sizemodeltypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
