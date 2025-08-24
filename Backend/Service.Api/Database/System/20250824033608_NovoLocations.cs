using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Api.Database.System
{
    /// <inheritdoc />
    public partial class NovoLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskLimits_Locations_LocationId",
                schema: "AppService",
                table: "RiskLimits");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskLimits_Risks_RiskId",
                schema: "AppService",
                table: "RiskLimits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskLimits",
                schema: "AppService",
                table: "RiskLimits");

            migrationBuilder.RenameTable(
                name: "RiskLimits",
                schema: "AppService",
                newName: "RiskLimit",
                newSchema: "AppService");

            migrationBuilder.RenameIndex(
                name: "IX_RiskLimits_RiskId",
                schema: "AppService",
                table: "RiskLimit",
                newName: "IX_RiskLimit_RiskId");

            migrationBuilder.RenameIndex(
                name: "IX_RiskLimits_LocationId",
                schema: "AppService",
                table: "RiskLimit",
                newName: "IX_RiskLimit_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskLimit",
                schema: "AppService",
                table: "RiskLimit",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LocationRisk",
                schema: "AppService",
                columns: table => new
                {
                    LocationsId = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskLimitsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationRisk", x => new { x.LocationsId, x.RiskLimitsId });
                    table.ForeignKey(
                        name: "FK_LocationRisk_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalSchema: "AppService",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationRisk_Risks_RiskLimitsId",
                        column: x => x.RiskLimitsId,
                        principalSchema: "AppService",
                        principalTable: "Risks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationRisk_RiskLimitsId",
                schema: "AppService",
                table: "LocationRisk",
                column: "RiskLimitsId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskLimit_Locations_LocationId",
                schema: "AppService",
                table: "RiskLimit",
                column: "LocationId",
                principalSchema: "AppService",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RiskLimit_Risks_RiskId",
                schema: "AppService",
                table: "RiskLimit",
                column: "RiskId",
                principalSchema: "AppService",
                principalTable: "Risks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskLimit_Locations_LocationId",
                schema: "AppService",
                table: "RiskLimit");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskLimit_Risks_RiskId",
                schema: "AppService",
                table: "RiskLimit");

            migrationBuilder.DropTable(
                name: "LocationRisk",
                schema: "AppService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskLimit",
                schema: "AppService",
                table: "RiskLimit");

            migrationBuilder.RenameTable(
                name: "RiskLimit",
                schema: "AppService",
                newName: "RiskLimits",
                newSchema: "AppService");

            migrationBuilder.RenameIndex(
                name: "IX_RiskLimit_RiskId",
                schema: "AppService",
                table: "RiskLimits",
                newName: "IX_RiskLimits_RiskId");

            migrationBuilder.RenameIndex(
                name: "IX_RiskLimit_LocationId",
                schema: "AppService",
                table: "RiskLimits",
                newName: "IX_RiskLimits_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskLimits",
                schema: "AppService",
                table: "RiskLimits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskLimits_Locations_LocationId",
                schema: "AppService",
                table: "RiskLimits",
                column: "LocationId",
                principalSchema: "AppService",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RiskLimits_Risks_RiskId",
                schema: "AppService",
                table: "RiskLimits",
                column: "RiskId",
                principalSchema: "AppService",
                principalTable: "Risks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
