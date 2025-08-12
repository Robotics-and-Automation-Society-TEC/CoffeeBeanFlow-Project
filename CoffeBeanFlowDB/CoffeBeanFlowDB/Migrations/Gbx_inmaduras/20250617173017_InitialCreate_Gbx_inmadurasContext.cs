using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeBeanFlowDB.Migrations.Gbx_inmaduras
{
    /// <inheritdoc />
    public partial class InitialCreate_Gbx_inmadurasContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gbx_inmaduras",
                columns: table => new
                {
                    ID_Gbx_inmaduras = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ID_inmaduras = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gbx_inmaduras", x => x.ID_Gbx_inmaduras);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gbx_inmaduras");
        }
    }
}
