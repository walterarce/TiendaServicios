using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TiendaServicios.Api.Autor.Migrations
{
    public partial class MigracionPostgresInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutorLibro",
                columns: table => new
                {
                    AutorLibroId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: true),
                    AutorLibroGuid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorLibro", x => x.AutorLibroId);
                });

            migrationBuilder.CreateTable(
                name: "GradoAcGradoAcademico",
                columns: table => new
                {
                    GraadoAcademicoId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(nullable: true),
                    CentroAcademico = table.Column<string>(nullable: true),
                    FechaGrado = table.Column<DateTime>(nullable: true),
                    AutorLibroId = table.Column<int>(nullable: false),
                    GradoAcademicoGuid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradoAcGradoAcademico", x => x.GraadoAcademicoId);
                    table.ForeignKey(
                        name: "FK_GradoAcGradoAcademico_AutorLibro_AutorLibroId",
                        column: x => x.AutorLibroId,
                        principalTable: "AutorLibro",
                        principalColumn: "AutorLibroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradoAcGradoAcademico_AutorLibroId",
                table: "GradoAcGradoAcademico",
                column: "AutorLibroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradoAcGradoAcademico");

            migrationBuilder.DropTable(
                name: "AutorLibro");
        }
    }
}
