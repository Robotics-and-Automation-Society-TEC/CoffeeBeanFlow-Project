using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeBeanFlowDB.Migrations.Formulario_Caracterizacion
{
    /// <inheritdoc />
    public partial class InitialCreate_Formulario_CaracterizacionContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Formulario_Caracterizacion",
                columns: table => new
                {
                    Tiempo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Cinmaduras = table.Column<int>(type: "integer", nullable: false),
                    Csobremaduras = table.Column<int>(type: "integer", nullable: false),
                    Csecas = table.Column<int>(type: "integer", nullable: false),
                    Cobjetivo = table.Column<int>(type: "integer", nullable: false),
                    Cverdes = table.Column<int>(type: "integer", nullable: false),
                    PCdebajo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Proceso = table.Column<string>(type: "text", nullable: false),
                    DRmaduras = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Mtabla = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PCverdes = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PCsecas = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PCencima = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Emaduracion = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Broca = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Densidad = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Vanos = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PCobjetivo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Secos = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Nlote_AreaAcopio = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formulario_Caracterizacion", x => x.Tiempo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Formulario_Caracterizacion");
        }
    }
}
