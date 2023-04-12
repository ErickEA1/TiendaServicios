using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TiendaServicios.Api.Libro.Migrations
{
    public partial class Libreria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LibreriasMaterial",
                columns: table => new
                {
                    LibreriaMaterialId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Titulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AutorLibro = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibreriasMaterial", x => x.LibreriaMaterialId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibreriasMaterial");
        }
    }
}
