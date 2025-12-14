using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeeBeanFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBodegaAndGuardarCafe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bodega",
                columns: table => new
                {
                    id_bodega = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nlote = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    w_bellota = table.Column<decimal>(type: "numeric", nullable: true),
                    w_pergamino = table.Column<decimal>(type: "numeric", nullable: true),
                    hfinal = table.Column<decimal>(type: "numeric", nullable: true),
                    hinicial = table.Column<decimal>(type: "numeric", nullable: true),
                    d_pergamino = table.Column<decimal>(type: "numeric", nullable: true),
                    d_bellota = table.Column<decimal>(type: "numeric", nullable: true),
                    finicio_reposo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cantidad_sacos = table.Column<int>(type: "integer", nullable: true),
                    pmh_relativa = table.Column<decimal>(type: "numeric", nullable: true),
                    pmt_interna = table.Column<decimal>(type: "numeric", nullable: true),
                    pmt_externa = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bodega", x => x.id_bodega);
                    table.ForeignKey(
                        name: "FK_bodega_area_acopio_nlote",
                        column: x => x.nlote,
                        principalTable: "area_acopio",
                        principalColumn: "nlote",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "guardar_cafe",
                columns: table => new
                {
                    id_secado = table.Column<int>(type: "integer", nullable: false),
                    id_bodega = table.Column<int>(type: "integer", nullable: false),
                    cantidad_sacos = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guardar_cafe", x => new { x.id_secado, x.id_bodega });
                    table.ForeignKey(
                        name: "FK_guardar_cafe_bodega_id_bodega",
                        column: x => x.id_bodega,
                        principalTable: "bodega",
                        principalColumn: "id_bodega",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_guardar_cafe_secado_id_secado",
                        column: x => x.id_secado,
                        principalTable: "secado",
                        principalColumn: "id_secado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bodega_nlote",
                table: "bodega",
                column: "nlote");

            migrationBuilder.CreateIndex(
                name: "IX_guardar_cafe_id_bodega",
                table: "guardar_cafe",
                column: "id_bodega");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guardar_cafe");

            migrationBuilder.DropTable(
                name: "bodega");
        }
    }
}
