using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GESINV.SubscriptionsService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Migracion004ProductoMainIdisuniquenow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Productos_ProductoMainId",
                table: "Productos",
                column: "ProductoMainId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Productos_ProductoMainId",
                table: "Productos");
        }
    }
}
