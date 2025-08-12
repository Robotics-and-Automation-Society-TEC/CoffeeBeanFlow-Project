using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeBeanFlowDB.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area_Acopio",
                columns: table => new
                {
                    Nlote = table.Column<string>(type: "text", nullable: false),
                    Rtotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Robjetivo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Rsobreobjetivo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Vendido = table.Column<bool>(type: "boolean", nullable: false),
                    Disponible = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Enproceso = table.Column<string>(type: "text", nullable: false),
                    Altura = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Zona = table.Column<string>(type: "text", nullable: false),
                    Nrecibo = table.Column<int>(type: "integer", nullable: false),
                    Nproductor = table.Column<string>(type: "text", nullable: false),
                    Nfinca = table.Column<string>(type: "text", nullable: false),
                    Despulpado = table.Column<string>(type: "text", nullable: false),
                    Psegundas = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PDmecanicos = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PPulpaPergamino = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PPergaminoPulpa = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DFruta = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Dpergamino_humedo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ID_Secado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area_Acopio", x => x.Nlote);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Area_Acopio");
        }
    }
}
