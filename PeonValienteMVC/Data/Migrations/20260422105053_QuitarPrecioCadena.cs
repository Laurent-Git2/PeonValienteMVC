using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeonValienteMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuitarPrecioCadena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioCadena",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Texto",
                table: "Productos");

            migrationBuilder.AlterColumn<bool>(
                name: "Escaparate",
                table: "Productos",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Escaparate",
                table: "Productos",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "PrecioCadena",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Texto",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
