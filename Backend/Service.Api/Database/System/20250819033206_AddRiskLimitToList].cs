using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Api.Database.System
{
    /// <inheritdoc />
    public partial class AddRiskLimitToList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_RiskLimits_Id",
                schema: "AppService",
                table: "Locations");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                schema: "AppService",
                table: "RiskLimits",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RiskLimits_LocationId",
                schema: "AppService",
                table: "RiskLimits",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskLimits_Locations_LocationId",
                schema: "AppService",
                table: "RiskLimits",
                column: "LocationId",
                principalSchema: "AppService",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskLimits_Locations_LocationId",
                schema: "AppService",
                table: "RiskLimits");

            migrationBuilder.DropIndex(
                name: "IX_RiskLimits_LocationId",
                schema: "AppService",
                table: "RiskLimits");

            migrationBuilder.DropColumn(
                name: "LocationId",
                schema: "AppService",
                table: "RiskLimits");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_RiskLimits_Id",
                schema: "AppService",
                table: "Locations",
                column: "Id",
                principalSchema: "AppService",
                principalTable: "RiskLimits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
