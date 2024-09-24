using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPlanner.Plannings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "sizemodels",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sizemodels_sizemodeltypes_SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "sizemodels");

            migrationBuilder.DropIndex(
                name: "IX_sizemodels_SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "sizemodels");

            migrationBuilder.AlterColumn<string>(
                name: "SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "sizemodels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
