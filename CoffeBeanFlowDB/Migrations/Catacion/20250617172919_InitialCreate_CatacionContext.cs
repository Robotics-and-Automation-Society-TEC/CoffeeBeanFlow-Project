using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeBeanFlowDB.Migrations.Catacion
{
    /// <inheritdoc />
    public partial class InitialCreate_CatacionContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catacion",
                columns: table => new
                {
                    ID_catacion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FFreposo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Nlote = table.Column<string>(type: "text", nullable: false),
                    Defectuoso = table.Column<bool>(type: "boolean", nullable: false),
                    Limpio = table.Column<bool>(type: "boolean", nullable: false),
                    Overde = table.Column<string>(type: "text", nullable: false),
                    CCcverde = table.Column<string>(type: "text", nullable: false),
                    Quaker = table.Column<int>(type: "integer", nullable: false),
                    Rtostado = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Dtueste = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CCcalidad = table.Column<int>(type: "integer", nullable: false),
                    Pfinales = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TAgtron = table.Column<int>(type: "integer", nullable: false),
                    C1negro = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    C1ME = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    C1insectos = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    C1cerezaseca = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    C1hongos = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    C1agrio = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    C2pergamino = table.Column<int>(type: "integer", nullable: false),
                    C2inmaduro = table.Column<int>(type: "integer", nullable: false),
                    C2negroP = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    C2agrioP = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    C2cascara_pulpa = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    C2insectos = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    C2averanado = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    C2partido_cortado_mordido = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    C2concha = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    C2flotador = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Trece = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Catorce = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Quince = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Dieciseis = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Diecisiete = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Dieciocho = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Diecinueve = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Veinte = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TresSobreDieciseis = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Residuo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catacion", x => x.ID_catacion);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catacion");
        }
    }
}
