using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class NewModuleForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Humidity",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Sensors");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sensors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "Sensors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SensorDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Humidity = table.Column<double>(type: "double precision", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SensorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorDatas_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_Id",
                table: "Sensors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_ModuleId",
                table: "Sensors",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Id",
                table: "Modules",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SensorDatas_Id",
                table: "SensorDatas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SensorDatas_SensorId",
                table: "SensorDatas",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Modules_ModuleId",
                table: "Sensors",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Modules_ModuleId",
                table: "Sensors");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "SensorDatas");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_Id",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_ModuleId",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "Sensors");

            migrationBuilder.AddColumn<double>(
                name: "Humidity",
                table: "Sensors",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "Sensors",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
