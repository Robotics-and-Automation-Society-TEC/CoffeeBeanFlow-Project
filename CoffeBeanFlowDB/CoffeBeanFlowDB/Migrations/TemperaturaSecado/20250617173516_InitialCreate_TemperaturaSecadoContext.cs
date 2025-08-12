using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeBeanFlowDB.Migrations.TemperaturaSecado
{
    /// <inheritdoc />
    public partial class InitialCreate_TemperaturaSecadoContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemperaturaSecado",
                columns: table => new
                {
                    ID_Temperatura = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Lectura = table.Column<int>(type: "integer", nullable: false),
                    ID_Secado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperaturaSecado", x => x.ID_Temperatura);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemperaturaSecado");
        }
    }
}
