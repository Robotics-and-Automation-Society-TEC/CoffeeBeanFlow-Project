using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeBeanFlowDB.Migrations.Trilla
{
    /// <inheritdoc />
    public partial class InitialCreate_TrillaContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trilla",
                columns: table => new
                {
                    ID_Trilla = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Psegundas = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Pmenudos = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Pinferiores = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Pmadres = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Pprimera = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Pcaracolillo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Pescogeduras = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Pbarreduras = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Pcataduras = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    POinferiores = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    RTeoricoSeleccion = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    RTeoricoPelado = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    RfinalPelado = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    RfinalSeleccion = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    WVerdeFinal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    WVerdeTeorico = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    FFinalReposo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HFinal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    HInicial = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Nlote = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trilla", x => x.ID_Trilla);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trilla");
        }
    }
}
