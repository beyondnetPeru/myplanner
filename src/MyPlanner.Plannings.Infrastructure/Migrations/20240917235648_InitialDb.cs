using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPlanner.Plannings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "myplanner-plannings");

            migrationBuilder.CreateTable(
                name: "errors",
                schema: "myplanner-plannings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_errors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationEventLog",
                schema: "myplanner-plannings",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    TimesSent = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLog", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "plans",
                schema: "myplanner-plannings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeModelTypeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeSpan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                schema: "myplanner-plannings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sizemodeltypes",
                schema: "myplanner-plannings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sizemodeltypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "planitems",
                schema: "myplanner-plannings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlanId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BusinessFeature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicalDefinition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentsImpacted = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicalDependencies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeModelTypeFactorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeModelTypeFactorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeModelTypeValueSelected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BallParkCost = table.Column<double>(type: "float", nullable: false),
                    BallParkDependenciesCost = table.Column<double>(type: "float", nullable: false),
                    BallParkTotalCost = table.Column<double>(type: "float", nullable: false),
                    KeyAssumptions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeSpan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planitems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_planitems_plans_PlanId",
                        column: x => x.PlanId,
                        principalSchema: "myplanner-plannings",
                        principalTable: "plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sizemodels",
                schema: "myplanner-plannings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SizeModelTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeSpan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sizemodels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sizemodels_sizemodeltypes_SizeModelTypeId",
                        column: x => x.SizeModelTypeId,
                        principalSchema: "myplanner-plannings",
                        principalTable: "sizemodeltypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sizemodeltypefactors",
                schema: "myplanner-plannings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SizeModelTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sizemodeltypefactors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sizemodeltypefactors_sizemodeltypes_SizeModelTypeId",
                        column: x => x.SizeModelTypeId,
                        principalSchema: "myplanner-plannings",
                        principalTable: "sizemodeltypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "sizemodelitems",
                schema: "myplanner-plannings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SizeModelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeModelTypeFactorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeModelTypeFactorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileAvgRateAmount = table.Column<double>(type: "float", nullable: false),
                    ProfileValueSelected = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeSpan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sizemodelitems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sizemodelitems_sizemodels_SizeModelId",
                        column: x => x.SizeModelId,
                        principalSchema: "myplanner-plannings",
                        principalTable: "sizemodels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_planitems_PlanId",
                schema: "myplanner-plannings",
                table: "planitems",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_sizemodelitems_SizeModelId",
                schema: "myplanner-plannings",
                table: "sizemodelitems",
                column: "SizeModelId");

            migrationBuilder.CreateIndex(
                name: "IX_sizemodels_SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "sizemodels",
                column: "SizeModelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_sizemodeltypefactors_SizeModelTypeId",
                schema: "myplanner-plannings",
                table: "sizemodeltypefactors",
                column: "SizeModelTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "errors",
                schema: "myplanner-plannings");

            migrationBuilder.DropTable(
                name: "IntegrationEventLog",
                schema: "myplanner-plannings");

            migrationBuilder.DropTable(
                name: "planitems",
                schema: "myplanner-plannings");

            migrationBuilder.DropTable(
                name: "requests",
                schema: "myplanner-plannings");

            migrationBuilder.DropTable(
                name: "sizemodelitems",
                schema: "myplanner-plannings");

            migrationBuilder.DropTable(
                name: "sizemodeltypefactors",
                schema: "myplanner-plannings");

            migrationBuilder.DropTable(
                name: "plans",
                schema: "myplanner-plannings");

            migrationBuilder.DropTable(
                name: "sizemodels",
                schema: "myplanner-plannings");

            migrationBuilder.DropTable(
                name: "sizemodeltypes",
                schema: "myplanner-plannings");
        }
    }
}
