using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeonValienteMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelacionIdeasColecciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coleccion",
                table: "Ideas");

            migrationBuilder.AddColumn<int>(
                name: "ColeccionId",
                table: "Ideas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_ColeccionId",
                table: "Ideas",
                column: "ColeccionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Colecciones_ColeccionId",
                table: "Ideas",
                column: "ColeccionId",
                principalTable: "Colecciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Colecciones_ColeccionId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_ColeccionId",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "ColeccionId",
                table: "Ideas");

            migrationBuilder.AddColumn<string>(
                name: "Coleccion",
                table: "Ideas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
