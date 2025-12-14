using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeBeanFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "area_acopio",
                columns: table => new
                {
                    nlote = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    altura = table.Column<decimal>(type: "numeric", nullable: false),
                    zona = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    nrecibo = table.Column<int>(type: "integer", nullable: false),
                    nproductor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nfinca = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    robjetivo = table.Column<decimal>(type: "numeric", nullable: true),
                    rtotal = table.Column<decimal>(type: "numeric", nullable: true),
                    vendido = table.Column<bool>(type: "boolean", nullable: false),
                    disponible = table.Column<decimal>(type: "numeric", nullable: true),
                    enproceso = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    semilavado = table.Column<bool>(type: "boolean", nullable: true),
                    natural = table.Column<bool>(type: "boolean", nullable: true),
                    anaerobico = table.Column<bool>(type: "boolean", nullable: true),
                    otro = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    miel = table.Column<bool>(type: "boolean", nullable: true),
                    lavado = table.Column<bool>(type: "boolean", nullable: true),
                    pf_pulpa_pergamino = table.Column<decimal>(type: "numeric", nullable: true),
                    pf_dmecanicos = table.Column<decimal>(type: "numeric", nullable: true),
                    pf_segundas = table.Column<decimal>(type: "numeric", nullable: true),
                    pf_pergamino_pulpa = table.Column<decimal>(type: "numeric", nullable: true),
                    pdensidad_fruta = table.Column<decimal>(type: "numeric", nullable: true),
                    pdensidad_pergamino_humedo = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_area_acopio", x => x.nlote);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "area_acopio");
        }
    }
}
