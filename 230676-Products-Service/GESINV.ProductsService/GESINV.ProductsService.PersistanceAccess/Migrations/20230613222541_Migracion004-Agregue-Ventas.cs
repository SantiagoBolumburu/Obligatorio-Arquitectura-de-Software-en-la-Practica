using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GESINV.ProductsService.PersistanceAccess.Migrations
{
    /// <inheritdoc />
    public partial class Migracion004AgregueVentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCompraProducto_Compras_CompraId",
                table: "DetalleCompraProducto");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCompraProducto_Productos_ProductoId",
                table: "DetalleCompraProducto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleCompraProducto",
                table: "DetalleCompraProducto");

            migrationBuilder.RenameTable(
                name: "DetalleCompraProducto",
                newName: "DetallesComprasProductos");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleCompraProducto_ProductoId",
                table: "DetallesComprasProductos",
                newName: "IX_DetallesComprasProductos_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleCompraProducto_CompraId",
                table: "DetallesComprasProductos",
                newName: "IX_DetallesComprasProductos_CompraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesComprasProductos",
                table: "DetallesComprasProductos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NombreCliente = table.Column<string>(type: "text", nullable: false),
                    FechaVenta = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MontoTotalEnPesos = table.Column<decimal>(type: "numeric", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetallesVentasProductos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uuid", nullable: false),
                    VentaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesVentasProductos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesVentasProductos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesVentasProductos_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVentasProductos_ProductoId",
                table: "DetallesVentasProductos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVentasProductos_VentaId",
                table: "DetallesVentasProductos",
                column: "VentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesComprasProductos_Compras_CompraId",
                table: "DetallesComprasProductos",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesComprasProductos_Productos_ProductoId",
                table: "DetallesComprasProductos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesComprasProductos_Compras_CompraId",
                table: "DetallesComprasProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesComprasProductos_Productos_ProductoId",
                table: "DetallesComprasProductos");

            migrationBuilder.DropTable(
                name: "DetallesVentasProductos");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesComprasProductos",
                table: "DetallesComprasProductos");

            migrationBuilder.RenameTable(
                name: "DetallesComprasProductos",
                newName: "DetalleCompraProducto");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesComprasProductos_ProductoId",
                table: "DetalleCompraProducto",
                newName: "IX_DetalleCompraProducto_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesComprasProductos_CompraId",
                table: "DetalleCompraProducto",
                newName: "IX_DetalleCompraProducto_CompraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleCompraProducto",
                table: "DetalleCompraProducto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCompraProducto_Compras_CompraId",
                table: "DetalleCompraProducto",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCompraProducto_Productos_ProductoId",
                table: "DetalleCompraProducto",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
