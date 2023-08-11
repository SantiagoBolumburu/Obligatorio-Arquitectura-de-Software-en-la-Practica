using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GESINV.SubscriptionsService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Migracion003AgregueRestricciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockSubscriptions_ProductoId",
                table: "StockSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CompraVentaSubscriptions_ProductoId",
                table: "CompraVentaSubscriptions");

            migrationBuilder.CreateIndex(
                name: "IX_StockSubscriptions_ProductoId_UsuarioId",
                table: "StockSubscriptions",
                columns: new[] { "ProductoId", "UsuarioId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompraVentaSubscriptions_ProductoId_UsuarioId",
                table: "CompraVentaSubscriptions",
                columns: new[] { "ProductoId", "UsuarioId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockSubscriptions_ProductoId_UsuarioId",
                table: "StockSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CompraVentaSubscriptions_ProductoId_UsuarioId",
                table: "CompraVentaSubscriptions");

            migrationBuilder.CreateIndex(
                name: "IX_StockSubscriptions_ProductoId",
                table: "StockSubscriptions",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraVentaSubscriptions_ProductoId",
                table: "CompraVentaSubscriptions",
                column: "ProductoId");
        }
    }
}
