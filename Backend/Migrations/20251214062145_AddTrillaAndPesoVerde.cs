using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeeBeanFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTrillaAndPesoVerde : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "trilla",
                columns: table => new
                {
                    id_trilla = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nlote = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    hinicial = table.Column<decimal>(type: "numeric", nullable: true),
                    hfinal = table.Column<decimal>(type: "numeric", nullable: true),
                    rfinal_pelado = table.Column<decimal>(type: "numeric", nullable: true),
                    rfinal_seleccion = table.Column<decimal>(type: "numeric", nullable: true),
                    wverde_final = table.Column<decimal>(type: "numeric", nullable: true),
                    rteorico_pelado = table.Column<decimal>(type: "numeric", nullable: true),
                    wverde_teorico = table.Column<decimal>(type: "numeric", nullable: true),
                    rteorico_seleccion = table.Column<decimal>(type: "numeric", nullable: true),
                    ffinal_reposo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    psegundas = table.Column<decimal>(type: "numeric", nullable: true),
                    pcataduras = table.Column<decimal>(type: "numeric", nullable: true),
                    pbarreduras = table.Column<decimal>(type: "numeric", nullable: true),
                    pescogeduras = table.Column<decimal>(type: "numeric", nullable: true),
                    pcaracolillo = table.Column<decimal>(type: "numeric", nullable: true),
                    pprimera = table.Column<decimal>(type: "numeric", nullable: true),
                    pmadres = table.Column<decimal>(type: "numeric", nullable: true),
                    pmenudos = table.Column<decimal>(type: "numeric", nullable: true),
                    pinferiores = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trilla", x => x.id_trilla);
                    table.ForeignKey(
                        name: "FK_trilla_area_acopio_nlote",
                        column: x => x.nlote,
                        principalTable: "area_acopio",
                        principalColumn: "nlote",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "peso_verde",
                columns: table => new
                {
                    id_peso_verde = table.Column<int>(type: "integer", nullable: false),
                    winferiores = table.Column<decimal>(type: "numeric", nullable: true),
                    wfinal = table.Column<decimal>(type: "numeric", nullable: true),
                    wfinal_inferiores = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_peso_verde", x => x.id_peso_verde);
                    table.ForeignKey(
                        name: "FK_peso_verde_trilla_id_peso_verde",
                        column: x => x.id_peso_verde,
                        principalTable: "trilla",
                        principalColumn: "id_trilla",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trilla_nlote",
                table: "trilla",
                column: "nlote");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "peso_verde");

            migrationBuilder.DropTable(
                name: "trilla");
        }
    }
}
