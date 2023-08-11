using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GESINV.ProductsService.PersistanceAccess.Migrations
{
    /// <inheritdoc />
    public partial class Migracion005AgregueVentasProgramadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Programada",
                table: "Ventas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Realizada",
                table: "Ventas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StockDespuesDeVenta",
                table: "DetallesVentasProductos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Programada",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "Realizada",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "StockDespuesDeVenta",
                table: "DetallesVentasProductos");
        }
    }
}
