using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GESINV.AthenticationService.PersistanceAccess.Migrations
{
    /// <inheritdoc />
    public partial class Migracion002AgregueApplicationKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InvitadorId",
                table: "Invitaciones",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ApplicationKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Activa = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ApplicationKeyStr = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationKeys", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitaciones_InvitadorId",
                table: "Invitaciones",
                column: "InvitadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitaciones_Usuarios_InvitadorId",
                table: "Invitaciones",
                column: "InvitadorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitaciones_Usuarios_InvitadorId",
                table: "Invitaciones");

            migrationBuilder.DropTable(
                name: "ApplicationKeys");

            migrationBuilder.DropIndex(
                name: "IX_Invitaciones_InvitadorId",
                table: "Invitaciones");

            migrationBuilder.DropColumn(
                name: "InvitadorId",
                table: "Invitaciones");
        }
    }
}
