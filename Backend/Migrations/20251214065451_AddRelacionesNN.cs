using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeBeanFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRelacionesNN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "enviar_muestras",
                columns: table => new
                {
                    id_trilla = table.Column<int>(type: "integer", nullable: false),
                    id_catacion = table.Column<int>(type: "integer", nullable: false),
                    ffinal_reposo = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enviar_muestras", x => new { x.id_trilla, x.id_catacion });
                    table.ForeignKey(
                        name: "FK_enviar_muestras_catacion_id_catacion",
                        column: x => x.id_catacion,
                        principalTable: "catacion",
                        principalColumn: "id_catacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_enviar_muestras_trilla_id_trilla",
                        column: x => x.id_trilla,
                        principalTable: "trilla",
                        principalColumn: "id_trilla",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suministra",
                columns: table => new
                {
                    id_bodega = table.Column<int>(type: "integer", nullable: false),
                    id_trilla = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suministra", x => new { x.id_bodega, x.id_trilla });
                    table.ForeignKey(
                        name: "FK_suministra_bodega_id_bodega",
                        column: x => x.id_bodega,
                        principalTable: "bodega",
                        principalColumn: "id_bodega",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suministra_trilla_id_trilla",
                        column: x => x.id_trilla,
                        principalTable: "trilla",
                        principalColumn: "id_trilla",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_enviar_muestras_id_catacion",
                table: "enviar_muestras",
                column: "id_catacion");

            migrationBuilder.CreateIndex(
                name: "IX_suministra_id_trilla",
                table: "suministra",
                column: "id_trilla");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enviar_muestras");

            migrationBuilder.DropTable(
                name: "suministra");
        }
    }
}
