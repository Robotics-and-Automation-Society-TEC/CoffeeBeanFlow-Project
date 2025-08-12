using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeBeanFlowDB.Migrations.TermoHigrometria
{
    /// <inheritdoc />
    public partial class InitialCreate_TermoHigrometriaContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TermoHigrometria",
                columns: table => new
                {
                    ID_Termo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Hrelativa = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Tinterna = table.Column<int>(type: "integer", nullable: false),
                    Texterna = table.Column<int>(type: "integer", nullable: false),
                    ID_Secado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermoHigrometria", x => x.ID_Termo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TermoHigrometria");
        }
    }
}
