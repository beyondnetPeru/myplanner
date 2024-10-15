using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPlanner.Plannings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeModelTypeSelected",
                schema: "myplanner-plannings",
                table: "sizemodelitems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SizeModelTypeSelected",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
