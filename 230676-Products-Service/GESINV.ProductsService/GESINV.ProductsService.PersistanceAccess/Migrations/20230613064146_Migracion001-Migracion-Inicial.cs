using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GESINV.ProductsService.PersistanceAccess.Migrations
{
    /// <inheritdoc />
    public partial class Migracion001MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    ImagenPath = table.Column<string>(type: "text", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric", nullable: false),
                    CantidadEnInventarioInicial = table.Column<int>(type: "integer", nullable: false),
                    Accesible = table.Column<bool>(type: "boolean", nullable: false),
                    CantidadVendida = table.Column<int>(type: "integer", nullable: false),
                    CantidadComprada = table.Column<int>(type: "integer", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_EmpresaId_Nombre",
                table: "Productos",
                columns: new[] { "EmpresaId", "Nombre" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
