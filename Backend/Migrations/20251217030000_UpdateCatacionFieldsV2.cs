using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCatacionFieldsV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar nuevas columnas
            migrationBuilder.AddColumn<decimal>(
                name: "c2negrop",
                table: "catacion",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "c2agriop",
                table: "catacion",
                type: "numeric",
                nullable: true);

            // Cambiar tipo de ffreposo (eliminar y recrear porque está vacía)
            migrationBuilder.DropColumn(
                name: "ffreposo",
                table: "catacion");

            migrationBuilder.AddColumn<DateTime>(
                name: "ffreposo",
                table: "catacion",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "c2negrop",
                table: "catacion");

            migrationBuilder.DropColumn(
                name: "c2agriop",
                table: "catacion");

            migrationBuilder.DropColumn(
                name: "ffreposo",
                table: "catacion");

            migrationBuilder.AddColumn<decimal>(
                name: "ffreposo",
                table: "catacion",
                type: "numeric",
                nullable: true);
        }
    }
}
