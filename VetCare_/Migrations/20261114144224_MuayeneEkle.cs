using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetCare_.Migrations
{
    /// <inheritdoc />
    public partial class MuayeneEkle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Muayeneler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    MuayeneTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MuayeneSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    YapilanIslem = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muayeneler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Muayeneler_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Muayeneler_PetId",
                table: "Muayeneler",
                column: "PetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Muayeneler");
        }
    }
}
