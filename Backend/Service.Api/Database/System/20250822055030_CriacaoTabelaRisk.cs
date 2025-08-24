using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Api.Database.System
{
    /// <inheritdoc />
    public partial class CriacaoTabelaRisk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimitValue",
                schema: "AppService",
                table: "RiskLimits");

            migrationBuilder.DropColumn(
                name: "RiskType",
                schema: "AppService",
                table: "RiskLimits");

            migrationBuilder.AddColumn<Guid>(
                name: "RiskId",
                schema: "AppService",
                table: "RiskLimits",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Risks",
                schema: "AppService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskType = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Category = table.Column<string>(type: "text", nullable: true),
                    SubCategory = table.Column<string>(type: "text", nullable: true),
                    Activity = table.Column<string>(type: "text", nullable: true),
                    MetabolicRate = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiskLimits_RiskId",
                schema: "AppService",
                table: "RiskLimits",
                column: "RiskId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskLimits_Risks_RiskId",
                schema: "AppService",
                table: "RiskLimits");

            migrationBuilder.DropTable(
                name: "Risks",
                schema: "AppService");

            migrationBuilder.DropIndex(
                name: "IX_RiskLimits_RiskId",
                schema: "AppService",
                table: "RiskLimits");

            migrationBuilder.DropColumn(
                name: "RiskId",
                schema: "AppService",
                table: "RiskLimits");

            migrationBuilder.AddColumn<double>(
                name: "LimitValue",
                schema: "AppService",
                table: "RiskLimits",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RiskType",
                schema: "AppService",
                table: "RiskLimits",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }
    }
}
