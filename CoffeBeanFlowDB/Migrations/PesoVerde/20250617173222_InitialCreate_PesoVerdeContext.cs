using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeBeanFlowDB.Migrations.PesoVerde
{
    /// <inheritdoc />
    public partial class InitialCreate_PesoVerdeContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PesoVerde",
                columns: table => new
                {
                    ID_PesoVerde = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Winferiores = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Wfinal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    WFinferior = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ID_PesoTrilla = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PesoVerde", x => x.ID_PesoVerde);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PesoVerde");
        }
    }
}
