using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeBeanFlowDB.Migrations.Bodega
{
    /// <inheritdoc />
    public partial class InitialCreate_BodegaContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bodega",
                columns: table => new
                {
                    ID_Bodega = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    D_Bellota = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    D_Pergamino = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Hinicial = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Hfinal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    W_pergamino = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    W_bellota = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    FinicioReposo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CantidadSacos = table.Column<int>(type: "integer", nullable: false),
                    PMTexterna = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PMTinterna = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PMH_relativa = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Nlote = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodega", x => x.ID_Bodega);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bodega");
        }
    }
}
