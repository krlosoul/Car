using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class myMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "personas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombres = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    apellidos = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    referencia = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.referencia);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPersona = table.Column<int>(type: "int", nullable: false),
                    usuario = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    clave = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuarios_personas_idPersona",
                        column: x => x.idPersona,
                        principalTable: "personas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carros",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    referenciaProducto = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carros", x => x.id);
                    table.ForeignKey(
                        name: "FK_carros_productos_referenciaProducto",
                        column: x => x.referenciaProducto,
                        principalTable: "productos",
                        principalColumn: "referencia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_carros_usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carros_idUsuario",
                table: "carros",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_carros_referenciaProducto",
                table: "carros",
                column: "referenciaProducto");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_idPersona",
                table: "usuarios",
                column: "idPersona",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carros");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "personas");
        }
    }
}
