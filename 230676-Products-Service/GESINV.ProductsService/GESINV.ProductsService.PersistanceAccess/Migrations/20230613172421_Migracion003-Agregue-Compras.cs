using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GESINV.ProductsService.PersistanceAccess.Migrations
{
    /// <inheritdoc />
    public partial class Migracion003AgregueCompras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CostoTotalEnPesos = table.Column<decimal>(type: "numeric", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProveedorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleCompraProducto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompraId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCompraProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleCompraProducto_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleCompraProducto_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_ProveedorId",
                table: "Compras",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompraProducto_CompraId",
                table: "DetalleCompraProducto",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompraProducto_ProductoId",
                table: "DetalleCompraProducto",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleCompraProducto");

            migrationBuilder.DropTable(
                name: "Compras");
        }
    }
}
