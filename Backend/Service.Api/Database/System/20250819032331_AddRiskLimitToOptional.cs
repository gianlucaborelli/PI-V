using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Api.Database.System
{
    /// <inheritdoc />
    public partial class AddRiskLimitToOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "AppService",
                table: "RiskLimits");

            migrationBuilder.AddColumn<string>(
                name: "RiskType",
                schema: "AppService",
                table: "RiskLimits",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RiskType",
                schema: "AppService",
                table: "RiskLimits");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "AppService",
                table: "RiskLimits",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");
        }
    }
}
