using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Api.Database.System
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AppService");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "AppService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                schema: "AppService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EspId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modules_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "AppService",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCompanies",
                schema: "AppService",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompanies", x => new { x.UserId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_UserCompanies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "AppService",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "AppService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ModuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalSchema: "AppService",
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleAccessTokens",
                schema: "AppService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleAccessTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleAccessTokens_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalSchema: "AppService",
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SensorDatas",
                schema: "AppService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Humidity = table.Column<double>(type: "double precision", nullable: false),
                    DryBulbTemperature = table.Column<double>(type: "double precision", nullable: false),
                    DarkBulbTemperature = table.Column<double>(type: "double precision", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorDatas_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "AppService",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ModuleId",
                schema: "AppService",
                table: "Locations",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleAccessTokens_ModuleId",
                schema: "AppService",
                table: "ModuleAccessTokens",
                column: "ModuleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CompanyId",
                schema: "AppService",
                table: "Modules",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorDatas_LocationId",
                schema: "AppService",
                table: "SensorDatas",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanies_CompanyId",
                schema: "AppService",
                table: "UserCompanies",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleAccessTokens",
                schema: "AppService");

            migrationBuilder.DropTable(
                name: "SensorDatas",
                schema: "AppService");

            migrationBuilder.DropTable(
                name: "UserCompanies",
                schema: "AppService");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "AppService");

            migrationBuilder.DropTable(
                name: "Modules",
                schema: "AppService");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "AppService");
        }
    }
}
