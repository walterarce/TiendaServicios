using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace TiendaServicios.Api.CarritoCompra.Migrations
{
    public partial class MigracionInicialMySQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarritoSession",
                columns: table => new
                {
                    CarritoSessionId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FechaCreacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoSession", x => x.CarritoSessionId);
                });

            migrationBuilder.CreateTable(
                name: "CarritoSessionDetalle",
                columns: table => new
                {
                    CarritoSessionDetalleId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    ProductoSeleccionado = table.Column<string>(nullable: true),
                    CarritoSessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoSessionDetalle", x => x.CarritoSessionDetalleId);
                    table.ForeignKey(
                        name: "FK_CarritoSessionDetalle_CarritoSession_CarritoSessionId",
                        column: x => x.CarritoSessionId,
                        principalTable: "CarritoSession",
                        principalColumn: "CarritoSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarritoSessionDetalle_CarritoSessionId",
                table: "CarritoSessionDetalle",
                column: "CarritoSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoSessionDetalle");

            migrationBuilder.DropTable(
                name: "CarritoSession");
        }
    }
}
