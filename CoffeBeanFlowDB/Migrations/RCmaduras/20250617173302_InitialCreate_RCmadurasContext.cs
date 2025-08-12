using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeBeanFlowDB.Migrations.RCmaduras
{
    /// <inheritdoc />
    public partial class InitialCreate_RCmadurasContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RCmaduras",
                columns: table => new
                {
                    ID_maduras = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Promedio = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: false),
                    Gbx = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Tiempo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RCmaduras", x => x.ID_maduras);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RCmaduras");
        }
    }
}
