using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeeBeanFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCaracterizacionAndRCs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "formulario_caracterizacion",
                columns: table => new
                {
                    tiempo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    nlote_area_acopio = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    dr_maduras = table.Column<decimal>(type: "numeric", nullable: true),
                    pc_debajo = table.Column<decimal>(type: "numeric", nullable: true),
                    proceso = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    l_asignado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    c_verdes = table.Column<int>(type: "integer", nullable: true),
                    c_objetivo = table.Column<int>(type: "integer", nullable: true),
                    c_inmaduras = table.Column<int>(type: "integer", nullable: true),
                    c_sobremaduras = table.Column<int>(type: "integer", nullable: true),
                    c_secas = table.Column<int>(type: "integer", nullable: true),
                    m_tabla = table.Column<decimal>(type: "numeric", nullable: true),
                    pc_verdes = table.Column<decimal>(type: "numeric", nullable: true),
                    pc_secas = table.Column<decimal>(type: "numeric", nullable: true),
                    pc_encima = table.Column<decimal>(type: "numeric", nullable: true),
                    e_maduracion = table.Column<decimal>(type: "numeric", nullable: true),
                    broca = table.Column<decimal>(type: "numeric", nullable: true),
                    densidad = table.Column<decimal>(type: "numeric", nullable: true),
                    vanos = table.Column<int>(type: "integer", nullable: true),
                    secos = table.Column<int>(type: "integer", nullable: true),
                    pc_objetivo = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formulario_caracterizacion", x => x.tiempo);
                    table.ForeignKey(
                        name: "FK_formulario_caracterizacion_area_acopio_nlote_area_acopio",
                        column: x => x.nlote_area_acopio,
                        principalTable: "area_acopio",
                        principalColumn: "nlote",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rc_inmaduras",
                columns: table => new
                {
                    id_inmaduras = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tiempo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gbx = table.Column<decimal>(type: "numeric", nullable: true),
                    promedio = table.Column<decimal>(type: "numeric", nullable: true),
                    observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rc_inmaduras", x => x.id_inmaduras);
                    table.ForeignKey(
                        name: "FK_rc_inmaduras_formulario_caracterizacion_tiempo",
                        column: x => x.tiempo,
                        principalTable: "formulario_caracterizacion",
                        principalColumn: "tiempo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rc_maduras",
                columns: table => new
                {
                    id_maduras = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tiempo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gbx = table.Column<decimal>(type: "numeric", nullable: true),
                    promedio = table.Column<decimal>(type: "numeric", nullable: true),
                    observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rc_maduras", x => x.id_maduras);
                    table.ForeignKey(
                        name: "FK_rc_maduras_formulario_caracterizacion_tiempo",
                        column: x => x.tiempo,
                        principalTable: "formulario_caracterizacion",
                        principalColumn: "tiempo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rc_sobremaduras",
                columns: table => new
                {
                    id_sobremaduras = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tiempo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gbx = table.Column<decimal>(type: "numeric", nullable: true),
                    promedio = table.Column<decimal>(type: "numeric", nullable: true),
                    observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rc_sobremaduras", x => x.id_sobremaduras);
                    table.ForeignKey(
                        name: "FK_rc_sobremaduras_formulario_caracterizacion_tiempo",
                        column: x => x.tiempo,
                        principalTable: "formulario_caracterizacion",
                        principalColumn: "tiempo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_formulario_caracterizacion_nlote_area_acopio",
                table: "formulario_caracterizacion",
                column: "nlote_area_acopio");

            migrationBuilder.CreateIndex(
                name: "IX_rc_inmaduras_tiempo",
                table: "rc_inmaduras",
                column: "tiempo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rc_maduras_tiempo",
                table: "rc_maduras",
                column: "tiempo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rc_sobremaduras_tiempo",
                table: "rc_sobremaduras",
                column: "tiempo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rc_inmaduras");

            migrationBuilder.DropTable(
                name: "rc_maduras");

            migrationBuilder.DropTable(
                name: "rc_sobremaduras");

            migrationBuilder.DropTable(
                name: "formulario_caracterizacion");
        }
    }
}
