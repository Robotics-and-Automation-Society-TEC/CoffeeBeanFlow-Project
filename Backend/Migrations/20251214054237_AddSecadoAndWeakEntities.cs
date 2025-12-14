using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeeBeanFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSecadoAndWeakEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "secado",
                columns: table => new
                {
                    id_secado = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nlote = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    finicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dsecado = table.Column<int>(type: "integer", nullable: true),
                    ffinal = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secado", x => x.id_secado);
                    table.ForeignKey(
                        name: "FK_secado_area_acopio_nlote",
                        column: x => x.nlote,
                        principalTable: "area_acopio",
                        principalColumn: "nlote",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "humedad",
                columns: table => new
                {
                    id_humedad = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_secado = table.Column<int>(type: "integer", nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    humedad = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_humedad", x => x.id_humedad);
                    table.ForeignKey(
                        name: "FK_humedad_secado_id_secado",
                        column: x => x.id_secado,
                        principalTable: "secado",
                        principalColumn: "id_secado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ncama",
                columns: table => new
                {
                    id_ncama = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_secado = table.Column<int>(type: "integer", nullable: false),
                    numero_cama = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ncama", x => x.id_ncama);
                    table.ForeignKey(
                        name: "FK_ncama_secado_id_secado",
                        column: x => x.id_secado,
                        principalTable: "secado",
                        principalColumn: "id_secado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "temperatura_secado",
                columns: table => new
                {
                    id_temperatura_secado = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_secado = table.Column<int>(type: "integer", nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    temperatura = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_temperatura_secado", x => x.id_temperatura_secado);
                    table.ForeignKey(
                        name: "FK_temperatura_secado_secado_id_secado",
                        column: x => x.id_secado,
                        principalTable: "secado",
                        principalColumn: "id_secado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "termo_higrometria",
                columns: table => new
                {
                    id_termo_higrometria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_secado = table.Column<int>(type: "integer", nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    temperatura_ambiente = table.Column<decimal>(type: "numeric", nullable: false),
                    humedad_ambiente = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_termo_higrometria", x => x.id_termo_higrometria);
                    table.ForeignKey(
                        name: "FK_termo_higrometria_secado_id_secado",
                        column: x => x.id_secado,
                        principalTable: "secado",
                        principalColumn: "id_secado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_humedad_id_secado",
                table: "humedad",
                column: "id_secado");

            migrationBuilder.CreateIndex(
                name: "IX_ncama_id_secado",
                table: "ncama",
                column: "id_secado");

            migrationBuilder.CreateIndex(
                name: "IX_secado_nlote",
                table: "secado",
                column: "nlote");

            migrationBuilder.CreateIndex(
                name: "IX_temperatura_secado_id_secado",
                table: "temperatura_secado",
                column: "id_secado");

            migrationBuilder.CreateIndex(
                name: "IX_termo_higrometria_id_secado",
                table: "termo_higrometria",
                column: "id_secado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "humedad");

            migrationBuilder.DropTable(
                name: "ncama");

            migrationBuilder.DropTable(
                name: "temperatura_secado");

            migrationBuilder.DropTable(
                name: "termo_higrometria");

            migrationBuilder.DropTable(
                name: "secado");
        }
    }
}
