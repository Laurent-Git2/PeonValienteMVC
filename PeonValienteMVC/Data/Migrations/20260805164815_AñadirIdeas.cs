using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeonValienteMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AñadirIdeas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ideas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FraseEspanol = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    FraseFrances = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Coleccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TipoProducto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Potencial = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ImagenTerminada = table.Column<bool>(type: "bit", nullable: false),
                    ArchivoPodTerminado = table.Column<bool>(type: "bit", nullable: false),
                    MockupTerminado = table.Column<bool>(type: "bit", nullable: false),
                    PublicadoEtsy = table.Column<bool>(type: "bit", nullable: false),
                    PublicadoPinterest = table.Column<bool>(type: "bit", nullable: false),
                    RutaImagen = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notas = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ideas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ideas");
        }
    }
}
