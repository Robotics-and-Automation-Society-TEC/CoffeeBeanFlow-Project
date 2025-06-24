using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoffeBeanFlowDB.Migrations.Registro_Formulario
{
    /// <inheritdoc />
    public partial class InitialCreate_Registro_FormularioContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registro_Formulario",
                columns: table => new
                {
                    ID_Formulario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_sobremaduras = table.Column<int>(type: "integer", nullable: false),
                    ID_maduras = table.Column<int>(type: "integer", nullable: false),
                    ID_inmaduras = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registro_Formulario", x => x.ID_Formulario);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registro_Formulario");
        }
    }
}
