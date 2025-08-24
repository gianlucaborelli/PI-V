using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Api.Database.System
{
    /// <inheritdoc />
    public partial class AddRiskLimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiskLimits",
                schema: "AppService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LimitValue = table.Column<double>(type: "double precision", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskLimits", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_RiskLimits_Id",
                schema: "AppService",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "RiskLimits",
                schema: "AppService");
        }
    }
}
