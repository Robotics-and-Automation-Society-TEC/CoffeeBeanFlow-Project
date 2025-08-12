using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeBeanFlowDB.Migrations.RCinmaduras
{
    /// <inheritdoc />
    public partial class InitialCreate_RCinmadurasContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RCinmaduras",
                columns: table => new
                {
                    ID_inmaduras = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Promedio = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: false),
                    Gbx = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Tiempo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RCinmaduras", x => x.ID_inmaduras);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RCinmaduras");
        }
    }
}
