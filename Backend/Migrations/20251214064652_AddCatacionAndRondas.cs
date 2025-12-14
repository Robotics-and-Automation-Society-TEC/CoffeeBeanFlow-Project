using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeeBeanFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCatacionAndRondas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "catacion",
                columns: table => new
                {
                    id_catacion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nlote = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    limpio = table.Column<decimal>(type: "numeric", nullable: true),
                    defectuoso = table.Column<decimal>(type: "numeric", nullable: true),
                    ffreposo = table.Column<decimal>(type: "numeric", nullable: true),
                    overde = table.Column<decimal>(type: "numeric", nullable: true),
                    quaker = table.Column<decimal>(type: "numeric", nullable: true),
                    ccverde = table.Column<decimal>(type: "numeric", nullable: true),
                    rtostado = table.Column<decimal>(type: "numeric", nullable: true),
                    dfueste = table.Column<decimal>(type: "numeric", nullable: true),
                    cccalidad = table.Column<decimal>(type: "numeric", nullable: true),
                    c1agrio = table.Column<decimal>(type: "numeric", nullable: true),
                    c1hongos = table.Column<decimal>(type: "numeric", nullable: true),
                    c1cerezaseca = table.Column<decimal>(type: "numeric", nullable: true),
                    c1negro = table.Column<decimal>(type: "numeric", nullable: true),
                    c1insectos = table.Column<decimal>(type: "numeric", nullable: true),
                    c1negrop = table.Column<decimal>(type: "numeric", nullable: true),
                    c1agriop = table.Column<decimal>(type: "numeric", nullable: true),
                    c1me = table.Column<decimal>(type: "numeric", nullable: true),
                    c2flotador = table.Column<decimal>(type: "numeric", nullable: true),
                    c2averanado = table.Column<decimal>(type: "numeric", nullable: true),
                    c2pergamino = table.Column<decimal>(type: "numeric", nullable: true),
                    c2inmaduro = table.Column<decimal>(type: "numeric", nullable: true),
                    c2concha = table.Column<decimal>(type: "numeric", nullable: true),
                    c2insectos = table.Column<decimal>(type: "numeric", nullable: true),
                    c2cascara = table.Column<decimal>(type: "numeric", nullable: true),
                    c2pulpa = table.Column<decimal>(type: "numeric", nullable: true),
                    c2partido = table.Column<decimal>(type: "numeric", nullable: true),
                    c2cortado = table.Column<decimal>(type: "numeric", nullable: true),
                    c2mordido = table.Column<decimal>(type: "numeric", nullable: true),
                    tres_sobre_dieciseis = table.Column<decimal>(type: "numeric", nullable: true),
                    veinte = table.Column<decimal>(type: "numeric", nullable: true),
                    diecinueve = table.Column<decimal>(type: "numeric", nullable: true),
                    dieciocho = table.Column<decimal>(type: "numeric", nullable: true),
                    diecisiete = table.Column<decimal>(type: "numeric", nullable: true),
                    dieciseis = table.Column<decimal>(type: "numeric", nullable: true),
                    quince = table.Column<decimal>(type: "numeric", nullable: true),
                    catorce = table.Column<decimal>(type: "numeric", nullable: true),
                    trece = table.Column<decimal>(type: "numeric", nullable: true),
                    tonagton_25 = table.Column<decimal>(type: "numeric", nullable: true),
                    tonagton_35 = table.Column<decimal>(type: "numeric", nullable: true),
                    tonagton_45 = table.Column<decimal>(type: "numeric", nullable: true),
                    tonagton_55 = table.Column<decimal>(type: "numeric", nullable: true),
                    tonagton_65 = table.Column<decimal>(type: "numeric", nullable: true),
                    tonagton_75 = table.Column<decimal>(type: "numeric", nullable: true),
                    tonagton_85 = table.Column<decimal>(type: "numeric", nullable: true),
                    tonagton_95 = table.Column<decimal>(type: "numeric", nullable: true),
                    pfinales = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catacion", x => x.id_catacion);
                });

            migrationBuilder.CreateTable(
                name: "rondas",
                columns: table => new
                {
                    id_rondas = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_catacion = table.Column<int>(type: "integer", nullable: false),
                    valor_calidad = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rondas", x => x.id_rondas);
                    table.ForeignKey(
                        name: "FK_rondas_catacion_id_catacion",
                        column: x => x.id_catacion,
                        principalTable: "catacion",
                        principalColumn: "id_catacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rondas_id_catacion",
                table: "rondas",
                column: "id_catacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rondas");

            migrationBuilder.DropTable(
                name: "catacion");
        }
    }
}
