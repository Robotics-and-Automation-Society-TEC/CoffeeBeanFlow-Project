using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeBeanFlowDB.Migrations.Secado
{
    /// <inheritdoc />
    public partial class InitialCreate_SecadoContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Secado",
                columns: table => new
                {
                    ID_Secado = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Finicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Dsecado = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Psolar = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Pmecanico = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Ffinal = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Nlote = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secado", x => x.ID_Secado);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Secado");
        }
    }
}
